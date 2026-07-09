import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { DepartmentService } from '../../../services/departmentservice';

@Component({
  selector: 'app-department-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './department-form.html',
  styleUrl: './department-form.css',
})
export class DepartmentForm implements OnInit {
  departmentForm!: FormGroup;
  loading = false;
  submitError = '';
  isEditMode = false;
  departmentId = 0;

  constructor(
    private fb: FormBuilder,
    private departmentService: DepartmentService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.departmentForm = this.fb.group({
      departmentName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.departmentId = Number(id);
      this.loadDepartmentById(this.departmentId);
    }
  }

  loadDepartmentById(id: number): void {
    this.departmentService.getDepartmentById(id).subscribe({
      next: (res) => {
        this.departmentForm.patchValue({
          departmentName: res.departmentName
        });
      },
      error: (err) => {
        this.submitError = err?.error || 'Failed to load department';
      }
    });
  }

  onSubmit(): void {
    if (this.departmentForm.invalid) {
      this.departmentForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.submitError = '';

    const payload = {
      departmentName: this.departmentForm.value.departmentName
    };

    if (this.isEditMode) {
      this.departmentService.updateDepartment(this.departmentId, payload).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/dashboard/departments']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to update department';
        }
      });
    } else {
      this.departmentService.addDepartment(payload).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/dashboard/departments']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to add department';
        }
      });
    }
  }

  get f() {
    return this.departmentForm.controls;
  }
}