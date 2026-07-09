import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface IDepartmentBudgetResponse {
  id: number;
  departmentId: number;
  departmentName: string;
  month: number;
  year: number;
  budgetAmount: number;
  isActive: boolean;
}

export interface IDepartmentBudgetRequest {
  departmentId: number;
  month: number;
  year: number;
  budgetAmount: number;
}

@Injectable({
  providedIn: 'root'
})
export class DepartmentBudgetService {
  private apiUrl = `${environment.apiBaseUrl}/Budget`;

  constructor(private http: HttpClient) {}

  getAllBudgets(): Observable<IDepartmentBudgetResponse[]> {
    return this.http.get<IDepartmentBudgetResponse[]>(`${this.apiUrl}/all`);
  }

  getBudget(departmentId: number, month: number, year: number): Observable<IDepartmentBudgetResponse> {
    return this.http.get<IDepartmentBudgetResponse>(`${this.apiUrl}/${departmentId}/${month}/${year}`);
  }

  addOrUpdateBudget(data: IDepartmentBudgetRequest): Observable<string> {
    return this.http.post(this.apiUrl, data, { responseType: 'text' });
  }

  deleteBudget(id: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
  }
}