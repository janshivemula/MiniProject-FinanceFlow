import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { EmployeeService, IEmployeeResponse } from '../../services/employeeservice';
import { ExpenseClaimService, IExpenseClaimResponse } from '../../services/expense-claim-service';
import { DepartmentBudgetService, IDepartmentBudgetResponse } from '../../services/department-budget-service';

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.css'
})
export class DashboardHome implements OnInit {
  loading = true;
  error = '';

  summaryCards = [
    { title: 'Total Employees', value: '0', icon: 'bi-people-fill', change: 'All employees' },
    { title: 'Pending Claims', value: '0', icon: 'bi-receipt-cutoff', change: 'Waiting for review' },
    { title: 'Approved Claims', value: '0', icon: 'bi-check-circle-fill', change: 'Approved expenses' },
    { title: 'Department Budgets', value: '0', icon: 'bi-wallet2', change: 'Budget records' }
  ];

  recentActivities: string[] = [];

  employees: IEmployeeResponse[] = [];
  claims: IExpenseClaimResponse[] = [];
  budgets: IDepartmentBudgetResponse[] = [];

  constructor(
    private employeeService: EmployeeService,
    private expenseService: ExpenseClaimService,
    private budgetService: DepartmentBudgetService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    let employeeDone = false;
    let claimDone = false;
    let budgetDone = false;

    this.loading = true;
    this.error = '';

    // Employees
    this.employeeService.getAllEmployees().subscribe({
      next: (res) => {
        this.employees = res;
        employeeDone = true;
        this.updateIfAllLoaded(employeeDone, claimDone, budgetDone);
      },
      error: () => {
        this.error = 'Failed to load dashboard data';
        this.loading = false;
      }
    });

    // Expense Claims
    this.expenseService.getAllClaims().subscribe({
      next: (res) => {
        this.claims = res;
        claimDone = true;
        this.updateIfAllLoaded(employeeDone, claimDone, budgetDone);
      },
      error: () => {
        this.error = 'Failed to load dashboard data';
        this.loading = false;
      }
    });

    // Budgets
    this.budgetService.getAllBudgets().subscribe({
      next: (res) => {
        this.budgets = res;
        budgetDone = true;
        this.updateIfAllLoaded(employeeDone, claimDone, budgetDone);
      },
      error: () => {
        this.error = 'Failed to load dashboard data';
        this.loading = false;
      }
    });
  }

  updateIfAllLoaded(empDone: boolean, claimDone: boolean, budgetDone: boolean): void {
    if (empDone && claimDone && budgetDone) {
      this.buildSummaryCards();
      this.buildRecentActivities();
      this.loading = false;
    }
  }

  buildSummaryCards(): void {
    const totalEmployees = this.employees.length;

    const pendingClaims = this.claims.filter(
      c => c.status?.toLowerCase() === 'pending'
    ).length;

    const approvedClaims = this.claims.filter(
      c => c.status?.toLowerCase() === 'approved'
    ).length;

    const totalBudgets = this.budgets.length;

    this.summaryCards = [
      {
        title: 'Total Employees',
        value: totalEmployees.toString(),
        icon: 'bi-people-fill',
        change: 'All employees'
      },
      {
        title: 'Pending Claims',
        value: pendingClaims.toString(),
        icon: 'bi-receipt-cutoff',
        change: 'Waiting for review'
      },
      {
        title: 'Approved Claims',
        value: approvedClaims.toString(),
        icon: 'bi-check-circle-fill',
        change: 'Approved expenses'
      },
      {
        title: 'Department Budgets',
        value: totalBudgets.toString(),
        icon: 'bi-wallet2',
        change: 'Budget records'
      }
    ];
  }

  buildRecentActivities(): void {
    const activity: string[] = [];

    // latest claims first by expense date
    const sortedClaims = [...this.claims].sort(
      (a, b) => new Date(b.expenseDate).getTime() - new Date(a.expenseDate).getTime()
    );

    for (const claim of sortedClaims.slice(0, 4)) {
      const status = claim.status?.toLowerCase();

      if (status === 'pending') {
        activity.push(
          `${claim.employeeName} submitted a ${claim.expenseCategoryName} expense claim`
        );
      } else if (status === 'approved') {
        activity.push(
          `${claim.expenseCategoryName} expense approved for ${claim.employeeName}`
        );
      } else if (status === 'rejected') {
        activity.push(
          `${claim.expenseCategoryName} expense rejected for ${claim.employeeName}`
        );
      }
    }

    // if claims are not enough, add budget entries
    if (activity.length < 4) {
      const latestBudgets = [...this.budgets].slice(-4).reverse();

      for (const budget of latestBudgets) {
        if (activity.length >= 4) break;
        activity.push(
          `${budget.departmentName} budget available for ${this.getMonthName(budget.month)} ${budget.year}`
        );
      }
    }

    // if still empty
    if (activity.length === 0) {
      activity.push('No recent activity found');
    }

    this.recentActivities = activity.slice(0, 4);
  }

  getMonthName(month: number): string {
    const months = [
      'January', 'February', 'March', 'April', 'May', 'June',
      'July', 'August', 'September', 'October', 'November', 'December'
    ];
    return months[month - 1] || '';
  }

  goToClaims(): void {
    this.router.navigate(['/dashboard/expense-claims']);
  }
}