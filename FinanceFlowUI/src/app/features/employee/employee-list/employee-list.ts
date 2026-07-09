import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { EmployeeService, IEmployeeResponse } from '../../../services/employeeservice';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.css'
})
export class EmployeeList implements OnInit {
  employees$: Observable<IEmployeeResponse[]> = of([]);
  errorMessage = '';

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.errorMessage = '';

    this.employees$ = this.employeeService.getAllEmployees().pipe(
      catchError((err) => {
        console.log('Employee load error:', err);
        this.errorMessage = 'Failed to load employees';
        return of([]);
      })
    );
  }

  deleteEmployee(id: number): void {
    const confirmDelete = confirm('Are you sure you want to deactivate this employee?');

    if (!confirmDelete) return;

    this.employeeService.deleteEmployee(id).subscribe({
      next: () => {
        this.loadEmployees();
      },
      error: () => {
        alert('Failed to deactivate employee');
      }
    });
  }

  getRoleName(role: number): string {
    return role === 1 ? 'Finance Manager' : 'Employee';
  }

  getStatusText(isActive: boolean): string {
    return isActive ? 'Active' : 'Inactive';
  }
}