import { Routes } from '@angular/router';

import { Login } from './layout/login/login';
import { Dashboard } from './layout/dashboard/dashboard';
import { DashboardHome } from './layout/dashboard-home/dashboard-home';

import { DepartmentList } from './features/department/department-list/department-list';

import { EmployeeList } from './features/employee/employee-list/employee-list';
import { EmployeeForm } from './features/employee/employee-form/employee-form';

import { ExpenseCategoryList } from './features/expense-category/expense-category-list/expense-category-list';
import { DepartmentBudgetList } from './features/department-budget/department-budget-list/department-budget-list';
import { ExpenseClaimList } from './features/expense-claim/expense-claim-list/expense-claim-list';
import { DepartmentForm } from './features/department/department-form/department-form';
import { ExpenseCategoryForm } from './features/expense-category/expense-category-form/expense-category-form';
import { DepartmentBudgetForm } from './features/department-budget/department-budget-form/department-budget-form';
import { ExpenseClaimForm } from './features/expense-claim/expense-claim-form/expense-claim-form';





export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: Login },

  {
    path: 'dashboard', component: Dashboard,
    children: [
      { path: '', component: DashboardHome },

      { path: 'departments', component: DepartmentList },
      { path: 'departments/add', component: DepartmentForm },
      { path: 'departments/edit/:id', component: DepartmentForm },

      { path: 'employees', component: EmployeeList },
      { path: 'employees/add', component: EmployeeForm },
      { path: 'employees/edit/:id', component: EmployeeForm },
      
      { path: 'expense-categories', component: ExpenseCategoryList },
      { path: 'expense-categories/add', component: ExpenseCategoryForm },
      { path: 'expense-categories/edit/:id', component: ExpenseCategoryForm },


      { path: 'department-budgets', component: DepartmentBudgetList },
      { path: 'department-budgets/add', component: DepartmentBudgetForm },
      { path: 'department-budgets/edit/:departmentId/:month/:year', component: DepartmentBudgetForm },

      { path: 'expense-claims', component: ExpenseClaimList },
      { path: 'expense-claims/add', component: ExpenseClaimForm },
      { path: 'expense-claims/edit/:id', component: ExpenseClaimForm },
    ]
  },

  { path: '**', redirectTo: 'login' }
];