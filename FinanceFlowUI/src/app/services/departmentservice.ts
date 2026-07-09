import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface IDepartmentResponse {
  id: number;
  departmentName: string;
  isActive: boolean;
}

export interface IDepartmentRequest {
  departmentName: string;
}

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private apiUrl = `${environment.apiBaseUrl}/Department`;

  constructor(private http: HttpClient) {}

  // Department list page -> all departments
  getAllDepartments(): Observable<IDepartmentResponse[]> {
    return this.http.get<IDepartmentResponse[]>(this.apiUrl);
  }

  // Dropdowns -> only active departments
  getActiveDepartments(): Observable<IDepartmentResponse[]> {
    return this.http.get<IDepartmentResponse[]>(`${this.apiUrl}/active`);
  }

  getDepartmentById(id: number): Observable<IDepartmentResponse> {
    return this.http.get<IDepartmentResponse>(`${this.apiUrl}/${id}`);
  }

  addDepartment(data: IDepartmentRequest): Observable<string> {
    return this.http.post(this.apiUrl, data, { responseType: 'text' });
  }

  updateDepartment(id: number, data: IDepartmentRequest): Observable<string> {
    return this.http.put(`${this.apiUrl}/${id}`, data, { responseType: 'text' });
  }

  deleteDepartment(id: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
  }
}