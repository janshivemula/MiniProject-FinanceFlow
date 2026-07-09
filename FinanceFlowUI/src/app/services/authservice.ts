import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../environments/environment'; 

export interface ILoginRequest {
  email: string;
  password: string;
}

export interface ILoginResponse {
  employeeId: number;
  employeeName: string;
  email: string;
  role: number;
  departmentId: number;
  departmentName: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiBaseUrl}/Login`;

  constructor(private http: HttpClient) {}

  login(data: ILoginRequest): Observable<ILoginResponse> {
    return this.http.post<ILoginResponse>(this.apiUrl, data).pipe(
      tap((res) => {
        localStorage.setItem('currentUser', JSON.stringify(res));
      })
    );
  }

  logout(): void {
    localStorage.removeItem('currentUser');
  }

  getCurrentUser(): ILoginResponse | null {
    const user = localStorage.getItem('currentUser');
    return user ? JSON.parse(user) : null;
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('currentUser');
  }
}
