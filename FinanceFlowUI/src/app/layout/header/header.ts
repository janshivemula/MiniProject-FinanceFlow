import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/authservice'; 


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  userName = 'User';
  roleName = 'Employee';

  constructor(private authService: AuthService, private router: Router) {
    const user = this.authService.getCurrentUser();

    if (user) {
      this.userName = user.employeeName;
      this.roleName = user.role === 1 ? 'Finance Manager' : 'Employee';
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}