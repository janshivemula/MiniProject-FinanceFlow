import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { ExpenseCategoryService } from '../../../services/expense-category-service';
import { IExpenseCategoryResponse } from '../../../services/expense-category-service'; 

@Component({
  selector: 'app-expense-category-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './expense-category-list.html',
  styleUrl: './expense-category-list.css'
})
export class ExpenseCategoryList implements OnInit {
  categories$: Observable<IExpenseCategoryResponse[]> = of([]);
  errorMessage = '';

  constructor(private categoryService: ExpenseCategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.errorMessage = '';
    this.categories$ = this.categoryService.getAllCategories();
  }

  deleteCategory(id: number): void {
    if (!confirm('Are you sure you want to deactivate this category?')) {
      return;
    }

    this.categoryService.deleteCategory(id).subscribe({
      next: () => {
        this.loadCategories();
      },
      error: (err) => {
        this.errorMessage = err?.error || 'Failed to deactivate category';
      }
    });
  }

  getStatusText(isActive: boolean): string {
    return isActive ? 'Active' : 'Inactive';
  }
}