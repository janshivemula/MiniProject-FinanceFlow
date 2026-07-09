import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { DepartmentBudgetService } from '../../../services/department-budget-service'; 
import { IDepartmentBudgetResponse } from '../../../services/department-budget-service';

@Component({
  selector: 'app-department-budget-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './department-budget-list.html',
  styleUrl: './department-budget-list.css'
})
export class DepartmentBudgetList implements OnInit {
  budgets$: Observable<IDepartmentBudgetResponse[]> = of([]);
  errorMessage = '';

  constructor(private budgetService: DepartmentBudgetService) {}

  ngOnInit(): void {
    this.loadBudgets();
  }

  loadBudgets(): void {
    this.errorMessage = '';
    this.budgets$ = this.budgetService.getAllBudgets();
  }

  deleteBudget(id: number): void {
    if (!confirm('Are you sure you want to deactivate this budget?')) {
      return;
    }

    this.budgetService.deleteBudget(id).subscribe({
      next: (res) => {
        alert(res);
        this.loadBudgets();
      },
      error: (err) => {
        this.errorMessage = err?.error || 'Failed to deactivate budget';
      }
    });
  }

  getStatusText(isActive: boolean): string {
    return isActive ? 'Active' : 'Inactive';
  }
}