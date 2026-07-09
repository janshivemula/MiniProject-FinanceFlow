import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ExpenseClaimService } from '../../../services/expense-claim-service';
import { IExpenseClaimResponse } from '../../../services/expense-claim-service'; 
import { AuthService } from '../../../services/authservice';

@Component({
  selector: 'app-expense-claim-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './expense-claim-list.html',
  styleUrl: './expense-claim-list.css'
})
export class ExpenseClaimList implements OnInit {
  claims: IExpenseClaimResponse[] = [];
  loading = true;
  errorMessage = '';

  currentUserId = 0;
  currentUserRole = 0; // 0 = Employee, 1 = Finance Manager (based on your project)

  constructor(
    private expenseService: ExpenseClaimService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const user = this.authService.getCurrentUser();
    if (user) {
      this.currentUserId = user.employeeId;
      this.currentUserRole = user.role;
    }

    this.loadClaims();
  }

  loadClaims(): void {
    this.loading = true;
    this.errorMessage = '';

    this.expenseService.getAllClaims().subscribe({
      next: (res) => {
        this.claims = res;
        this.loading = false;
      },
      error: (err) => {
        console.log('Expense claim load error:', err);
        this.errorMessage = 'Failed to load expense claims';
        this.loading = false;
      }
    });
  }

  canEditOrDelete(claim: IExpenseClaimResponse): boolean {
    return claim.status === 'Pending';
  }

  canApproveReject(claim: IExpenseClaimResponse): boolean {
    return this.currentUserRole === 1 && claim.status === 'Pending';
  }

  deleteClaim(id: number): void {
    const confirmDelete = confirm('Are you sure you want to delete this expense claim?');
    if (!confirmDelete) return;

    this.expenseService.deleteClaim(id).subscribe({
      next: (res) => {
        alert(res);
        this.loadClaims();
      },
      error: (err) => {
        console.log(err);
        alert(err?.error || 'Failed to delete expense claim');
      }
    });
  }

  approveClaim(claim: IExpenseClaimResponse): void {
    const remark = prompt('Enter approval remark:') || '';
    if (remark.trim() === '') {
      alert('Approval remark is required');
      return;
    }

    this.expenseService.approveClaim(claim.id, this.currentUserId, remark).subscribe({
      next: (res) => {
        alert(res);
        this.loadClaims();
      },
      error: (err) => {
        console.log(err);
        alert(err?.error || 'Failed to approve claim');
      }
    });
  }

  rejectClaim(claim: IExpenseClaimResponse): void {
    const remark = prompt('Enter rejection remark:') || '';
    if (remark.trim() === '') {
      alert('Rejection remark is required');
      return;
    }

    this.expenseService.rejectClaim(claim.id, this.currentUserId, remark).subscribe({
      next: (res) => {
        alert(res);
        this.loadClaims();
      },
      error: (err) => {
        console.log(err);
        alert(err?.error || 'Failed to reject claim');
      }
    });
  }
}