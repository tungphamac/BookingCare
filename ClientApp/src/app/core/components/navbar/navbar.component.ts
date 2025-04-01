import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { User } from '../../../features/auth/login/Models/user.model';
import { AuthService } from '../../../features/auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true, // Thêm standalone nếu cần
  imports: [FormsModule, RouterLink, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  user?: User;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    // Lấy user từ AuthService thông qua Observable
    this.authService.user().subscribe({
      next: response => {
        this.user = response;
      },
      error: err => {
        console.error('Error fetching user:', err);
        this.user = undefined;
      }
    });

    // Lấy user từ AuthService (nếu đã lưu trong localStorage hoặc token)
    this.user = this.authService.getUser();
  }

  onLogout(): void {
    this.authService.logout();
    this.user = undefined;
  }
}