import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { DepartmentBudgetService } from '../../../services/department-budget-service'; 
import { IDepartmentBudgetRequest } from '../../../services/department-budget-service';
import { DepartmentService, IDepartmentResponse } from '../../../services/departmentservice';

@Component({
  selector: 'app-department-budget-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './department-budget-form.html',
  styleUrl: './department-budget-form.css'
})
export class DepartmentBudgetForm implements OnInit {
  budgetForm!: FormGroup;
  departments: IDepartmentResponse[] = [];
  loading = false;
  submitError = '';
  isEditMode = false;

  originalDepartmentId!: number;
  originalMonth!: number;
  originalYear!: number;

  constructor(
    private fb: FormBuilder,
    private budgetService: DepartmentBudgetService,
    private departmentService: DepartmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.budgetForm = this.fb.group({
      departmentId: ['', Validators.required],
      month: ['', [Validators.required, Validators.min(1), Validators.max(12)]],
      year: ['', [Validators.required, Validators.min(2000)]],
      budgetAmount: ['', [Validators.required, Validators.min(1)]]
    });

    this.loadDepartments();

    const departmentId = this.route.snapshot.paramMap.get('departmentId');
    const month = this.route.snapshot.paramMap.get('month');
    const year = this.route.snapshot.paramMap.get('year');

    if (departmentId && month && year) {
      this.isEditMode = true;
      this.originalDepartmentId = Number(departmentId);
      this.originalMonth = Number(month);
      this.originalYear = Number(year);

      this.loadBudgetForEdit(this.originalDepartmentId, this.originalMonth, this.originalYear);
    }
  }

  loadDepartments(): void {
    this.departmentService.getAllDepartments().subscribe({
      next: (res) => {
        this.departments = res.filter(d => d.isActive);
      },
      error: () => {
        this.submitError = 'Failed to load departments';
      }
    });
  }

  loadBudgetForEdit(departmentId: number, month: number, year: number): void {
    this.budgetService.getBudget(departmentId, month, year).subscribe({
      next: (res) => {
        if (!res.isActive) {
          this.submitError = 'Inactive budget cannot be edited';
          this.budgetForm.disable();
          return;
        }

        this.budgetForm.patchValue({
          departmentId: res.departmentId,
          month: res.month,
          year: res.year,
          budgetAmount: res.budgetAmount
        });
      },
      error: () => {
        this.submitError = 'Failed to load budget details';
      }
    });
  }

  onSubmit(): void {
    if (this.budgetForm.invalid) {
      this.budgetForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.submitError = '';

    const payload: IDepartmentBudgetRequest = {
      departmentId: Number(this.budgetForm.value.departmentId),
      month: Number(this.budgetForm.value.month),
      year: Number(this.budgetForm.value.year),
      budgetAmount: Number(this.budgetForm.value.budgetAmount)
    };

    this.budgetService.addOrUpdateBudget(payload).subscribe({
      next: (res) => {
        this.loading = false;
        alert(res);
        this.router.navigate(['/dashboard/department-budgets']);
      },
      error: (err) => {
        this.loading = false;
        this.submitError = err?.error || 'Failed to save budget';
      }
    });
  }

  get f() {
    return this.budgetForm.controls;
  }
}
