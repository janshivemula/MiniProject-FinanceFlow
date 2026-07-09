import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ExpenseCategoryService } from '../../../services/expense-category-service';

@Component({
  selector: 'app-expense-category-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './expense-category-form.html',
  styleUrl: './expense-category-form.css'
})
export class ExpenseCategoryForm implements OnInit {
  categoryForm!: FormGroup;
  loading = false;
  submitError = '';
  isEditMode = false;
  categoryId = 0;

  constructor(
    private fb: FormBuilder,
    private categoryService: ExpenseCategoryService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.categoryForm = this.fb.group({
      categoryName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.categoryId = Number(id);
      this.loadCategoryById(this.categoryId);
    }
  }

  loadCategoryById(id: number): void {
    this.categoryService.getCategoryById(id).subscribe({
      next: (res) => {
        this.categoryForm.patchValue({
          categoryName: res.categoryName
        });
      },
      error: (err) => {
        this.submitError = err?.error || 'Failed to load category';
      }
    });
  }

  onSubmit(): void {
    if (this.categoryForm.invalid) {
      this.categoryForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.submitError = '';

    const payload = {
      categoryName: this.categoryForm.value.categoryName
    };

    if (this.isEditMode) {
      this.categoryService.updateCategory(this.categoryId, payload).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/dashboard/expense-categories']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to update category';
        }
      });
    } else {
      this.categoryService.addCategory(payload).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/dashboard/expense-categories']);
        },
        error: (err) => {
          this.loading = false;
          this.submitError = err?.error || 'Failed to add category';
        }
      });
    }
  }

  get f() {
    return this.categoryForm.controls;
  }
}