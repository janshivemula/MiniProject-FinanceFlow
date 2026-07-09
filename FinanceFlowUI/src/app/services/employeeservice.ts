import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface IEmployeeResponse {
  employeeId: number;
  employeeName: string;
  email: string;
  phoneNumber: string;
  departmentName: string;
  role: number;
  isActive: boolean;
}

export interface IEmployeeRequest {
  employeeName: string;
  email: string;
  phoneNumber: string;
  password: string;
  departmentId: number;
  role: number;
}

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = `${environment.apiBaseUrl}/Employee`;

  constructor(private http: HttpClient) {}

  getAllEmployees(): Observable<IEmployeeResponse[]> {
    return this.http.get<IEmployeeResponse[]>(this.apiUrl);
  }

  getEmployeeById(id: number): Observable<IEmployeeResponse> {
    return this.http.get<IEmployeeResponse>(`${this.apiUrl}/${id}`);
  }

  addEmployee(data: IEmployeeRequest): Observable<string> {
    return this.http.post(this.apiUrl, data, { responseType: 'text' });
  }

  updateEmployee(id: number, data: IEmployeeRequest): Observable<string> {
    return this.http.put(`${this.apiUrl}/${id}`, data, { responseType: 'text' });
  }

  deleteEmployee(id: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
  }
}