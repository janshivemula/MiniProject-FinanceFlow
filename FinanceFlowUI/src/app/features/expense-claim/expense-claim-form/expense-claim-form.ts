import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { ExpenseClaimService } from '../../../services/expense-claim-service';
import { EmployeeService, IEmployeeResponse } from '../../../services/employeeservice';
import { ExpenseCategoryService, IExpenseCategoryResponse } from '../../../services/expense-category-service';
import { AuthService } from '../../../services/authservice';

@Component({
  selector: 'app-expense-claim-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './expense-claim-form.html',
  styleUrl: './expense-claim-form.css'
})
export class ExpenseClaimForm implements OnInit {
  expenseForm!: FormGroup;
  employees: IEmployeeResponse[] = [];
  categories: IExpenseCategoryResponse[] = [];

  loading = false;
  pageLoading = true;
  submitError = '';

  isEditMode = false;
  claimId = 0;

  currentUserId = 0;
  currentUserRole = 0;

  constructor(
    private fb: FormBuilder,
    private expenseService: ExpenseClaimService,
    private employeeService: EmployeeService,
    private categoryService: ExpenseCategoryService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const user = this.authService.getCurrentUser();
    if (user) {
      this.currentUserId = user.employeeId;
      this.currentUserRole = user.role;
    }

    this.expenseForm = this.fb.group({
      employeeId: ['', Validators.required],
      expenseCategoryId: ['', Validators.required],
      amount: ['', [Validators.required, Validators.min(1)]],
      expenseDate: ['', Validators.required],
      description: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(250)]]
    });

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.claimId = Number(idParam);
    }

    this.loadInitialData();
  }

  loadInitialData(): void {
    let employeeDone = false;
    let categoryDone = false;
    let claimDone = !this.isEditMode;

    this.employeeService.getAllEmployees().subscribe({
      next: (res) => {
        this.employees = res.filter(e => e.isActive);
        employeeDone = true;
        this.checkPageLoaded(employeeDone, categoryDone, claimDone);

        if (!this.isEditMode && this.currentUserRole !== 1) {
          this.expenseForm.patchValue({ employeeId: this.currentUserId });
          this.expenseForm.get('employeeId')?.disable();
        }
      },
      error: () => {
        this.submitError = 'Failed to load employees';
        this.pageLoading = false;
      }
    });

    this.categoryService.getActiveCategories().subscribe({
      next: (res) => {
        this.categories = res;
        categoryDone = true;
        this.checkPageLoaded(employeeDone, categoryDone, claimDone);
      },
      error: () => {
        this.submitError = 'Failed to load expense categories';
        this.pageLoading = false;
      }
    });

    if (this.isEditMode) {
      this.expenseService.getClaimById(this.claimId).subscribe({
        next: (res) => {
          this.expenseForm.patchValue({
            employeeId: res.employeeId,
            expenseCategoryId: res.expenseCategoryId,
            amount: res.amount,
            expenseDate: this.formatDateForInput(res.expenseDate),
            description: res.description
          });

          if (this.currentUserRole !== 1) {
            this.expenseForm.get('employeeId')?.disable();
          }

          claimDone = true;
          this.checkPageLoaded(employeeDone, categoryDone, claimDone);
        },
        error: () => {
          this.submitError = 'Failed to load expense claim';
          this.pageLoading = false;
        }
      });
    }
  }

  checkPageLoaded(emp: boolean, cat: boolean, claim: boolean): void {
    if (emp && cat && claim) {
      this.pageLoading = false;
    }
  }

  formatDateForInput(dateValue: string): string {
    const date = new Date(dateValue);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  onSubmit(): void {
    if (this.expenseForm.invalid) {
      this.expenseForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.submitError = '';

    const rawValue = this.expenseForm.getRawValue();

    const payload = {
      employeeId: Number(rawValue.employeeId),
      expenseCategoryId: Number(rawValue.expenseCategoryId),
      amount: Number(rawValue.amount),
      expenseDate: rawValue.expenseDate,
      description: rawValue.description
    };

    if (this.isEditMode) {
      this.expenseService.updateClaim(this.claimId, payload).subscribe({
        next: (res) => {
          this.loading = false;
          alert(res);
          this.router.navigate(['/dashboard/expense-claims']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to update expense claim';
        }
      });
    } else {
      this.expenseService.addClaim(payload).subscribe({
        next: (res) => {
          this.loading = false;
          alert(res);
          this.router.navigate(['/dashboard/expense-claims']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to add expense claim';
        }
      });
    }
  }

  get f() {
    return this.expenseForm.controls;
  }
}