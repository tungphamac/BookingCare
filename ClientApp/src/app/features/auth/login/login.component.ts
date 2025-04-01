import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginRequest } from './Models/login-request.model';
import { AuthService } from '../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true, // Thêm standalone
  imports: [FormsModule, RouterModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: LoginRequest;
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.model = {
      email: '',
      password: ''
    };
  }

  onFormSubmit() {
    if (!this.model.email || !this.model.password) {
      this.errorMessage = 'Email và mật khẩu là bắt buộc';
      return;
    }

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(this.model.email)) {
      this.errorMessage = 'Email không hợp lệ';
      return;
    }

    if (this.model.password.length < 6) {
      this.errorMessage = 'Mật khẩu phải có ít nhất 6 ký tự';
      return;
    }

    this.errorMessage = '';
    this.isLoading = true;

    this.authService.login(this.model).subscribe({
      next: (response) => {
        this.cookieService.set(
          'Authentication',
          `${response.token}`,
          undefined,
          '/',
          undefined,
          true,
          'Strict'
        );
        // Sử dụng id và email từ response
        this.authService.setUser({ email: response.email, id: response.id });
        this.router.navigateByUrl('/');
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        if (err.status === 401) {
          this.errorMessage = 'Email hoặc mật khẩu không đúng';
        } else if (err.status === 400) {
          this.errorMessage = err.error.message || 'Dữ liệu không hợp lệ';
        } else {
          this.errorMessage = 'Đăng nhập thất bại. Vui lòng thử lại sau!';
        }
      }
    });
  }
}