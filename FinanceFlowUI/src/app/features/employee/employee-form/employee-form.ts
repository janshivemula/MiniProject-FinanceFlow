import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../../services/employeeservice';
import { DepartmentService } from '../../../services/departmentservice';
import { IDepartmentResponse } from '../../../services/departmentservice';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './employee-form.html',
  styleUrl: './employee-form.css'
})
export class EmployeeForm implements OnInit {
  employeeForm!: FormGroup;
  departments: IDepartmentResponse[] = [];
  loading = false;
  submitError = '';

  isEditMode = false;
  employeeId = 0;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.employeeForm = this.fb.group({
      employeeName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,15}$/)]],
      departmentId: ['', Validators.required],
      role: ['', Validators.required]
    });

    this.loadDepartments();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.employeeId = Number(id);
      this.loadEmployeeById(this.employeeId);
    }
  }

  loadDepartments(): void {
  this.departmentService.getActiveDepartments().subscribe({
    next: (res) => {
      this.departments = res;
    },
    error: () => {
      this.submitError = 'Failed to load departments';
    }
  });
}

  loadEmployeeById(id: number): void {
    this.employeeService.getEmployeeById(id).subscribe({
      next: (emp) => {
        this.employeeForm.patchValue({
          employeeName: emp.employeeName,
          email: emp.email,
          phoneNumber: emp.phoneNumber,
          password: '',
          role: emp.role
        });

        const matchingDepartment = this.departments.find(
          d => d.departmentName === emp.departmentName
        );

        if (matchingDepartment) {
          this.employeeForm.patchValue({
            departmentId: matchingDepartment.id
          });
        }
      },
      error: () => {
        this.submitError = 'Failed to load employee details';
      }
    });
  }

  onSubmit(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.submitError = '';

    const payload = {
      ...this.employeeForm.value,
      departmentId: Number(this.employeeForm.value.departmentId),
      role: Number(this.employeeForm.value.role)
    };

    if (this.isEditMode) {
      this.employeeService.updateEmployee(this.employeeId, payload).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/dashboard/employees']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to update employee';
        }
      });
    } else {
      this.employeeService.addEmployee(payload).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/dashboard/employees']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to add employee';
        }
      });
    }
  }

  get f() {
    return this.employeeForm.controls;
  }
}