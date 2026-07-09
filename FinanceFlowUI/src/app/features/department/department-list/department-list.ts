import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { DepartmentService, IDepartmentResponse } from '../../../services/departmentservice';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './department-list.html',
  styleUrl: './department-list.css',
})
export class DepartmentList implements OnInit {
  departments$: Observable<IDepartmentResponse[]> = of([]);
  errorMessage = '';

  constructor(private departmentService: DepartmentService) {}

  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.errorMessage = '';
    this.departments$ = this.departmentService.getAllDepartments();
  }

  deleteDepartment(id: number): void {
    if (!confirm('Are you sure you want to deactivate this department?')) {
      return;
    }

    this.departmentService.deleteDepartment(id).subscribe({
      next: () => {
        this.loadDepartments();
      },
      error: (err) => {
        this.errorMessage = err?.error || 'Failed to deactivate department';
      }
    });
  }

  getStatusText(isActive: boolean): string {
    return isActive ? 'Active' : 'Inactive';
  }
}