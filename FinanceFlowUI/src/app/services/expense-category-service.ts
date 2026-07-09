import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface IExpenseCategoryResponse {
  id: number;
  categoryName: string;
  isActive: boolean;
}

export interface IExpenseCategoryRequest {
  categoryName: string;
}

@Injectable({
  providedIn: 'root'
})
export class ExpenseCategoryService {
  private apiUrl = `${environment.apiBaseUrl}/Category`;

  constructor(private http: HttpClient) {}

  // List page -> all
  getAllCategories(): Observable<IExpenseCategoryResponse[]> {
    return this.http.get<IExpenseCategoryResponse[]>(this.apiUrl);
  }

  // Dropdown -> only active
  getActiveCategories(): Observable<IExpenseCategoryResponse[]> {
    return this.http.get<IExpenseCategoryResponse[]>(`${this.apiUrl}/active`);
  }

  getCategoryById(id: number): Observable<IExpenseCategoryResponse> {
    return this.http.get<IExpenseCategoryResponse>(`${this.apiUrl}/${id}`);
  }

  addCategory(data: IExpenseCategoryRequest): Observable<string> {
    return this.http.post(this.apiUrl, data, { responseType: 'text' });
  }

  updateCategory(id: number, data: IExpenseCategoryRequest): Observable<string> {
    return this.http.put(`${this.apiUrl}/${id}`, data, { responseType: 'text' });
  }

  deleteCategory(id: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
  }
}