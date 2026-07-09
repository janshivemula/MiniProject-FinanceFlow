import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export enum ExpenseStatus {
  Pending = 0,
  Approved = 1,
  Rejected = 2
}

export interface IExpenseClaimRequest {
  employeeId: number;
  expenseCategoryId: number;
  amount: number;
  expenseDate: string;
  description: string;
}

export interface IExpenseClaimResponse {
  id: number;
  employeeId: number;
  employeeName: string;
  departmentId: number;
  departmentName: string;
  expenseCategoryId: number;
  expenseCategoryName: string;
  amount: number;
  expenseDate: string;
  description: string;
  status: string;
  reviewRemark?: string;
  approvedBy?: string;
  approvedDate?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ExpenseClaimService {
  private apiUrl = `${environment.apiBaseUrl}/Expense`;

  constructor(private http: HttpClient) {}

  getAllClaims(): Observable<IExpenseClaimResponse[]> {
    return this.http.get<IExpenseClaimResponse[]>(this.apiUrl);
  }

  getClaimById(id: number): Observable<IExpenseClaimResponse> {
    return this.http.get<IExpenseClaimResponse>(`${this.apiUrl}/${id}`);
  }

  addClaim(data: IExpenseClaimRequest): Observable<string> {
    return this.http.post(this.apiUrl, data, { responseType: 'text' });
  }

  updateClaim(id: number, data: IExpenseClaimRequest): Observable<string> {
    return this.http.put(`${this.apiUrl}/${id}`, data, { responseType: 'text' });
  }

  deleteClaim(id: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
  }

  approveClaim(id: number, employeeId: number, remark: string): Observable<string> {
    return this.http.put(
      `${this.apiUrl}/approve/${id}?employeeId=${employeeId}&remark=${encodeURIComponent(remark)}`,
      {},
      { responseType: 'text' }
    );
  }

  rejectClaim(id: number, employeeId: number, remark: string): Observable<string> {
    return this.http.put(
      `${this.apiUrl}/reject/${id}?employeeId=${employeeId}&remark=${encodeURIComponent(remark)}`,
      {},
      { responseType: 'text' }
    );
  }
}