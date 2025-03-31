import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginRequest } from './Models/login-request.model';
import { AuthService } from '../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
<<<<<<< HEAD
=======
import { CommonModule } from '@angular/common';
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06


@Component({
  selector: 'app-login',
<<<<<<< HEAD
  imports: [FormsModule, RouterModule],
=======
  imports: [FormsModule, RouterModule, CommonModule],
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginRequest;
<<<<<<< HEAD

=======
  errorMessage: string = '';
  isLoading: boolean = false;
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
  constructor(private authService: AuthService, private cookieService: CookieService, private router: Router) {
    this.model = {
      email: '',
      password: ''
    }
  }

  onFormSubmit() {
<<<<<<< HEAD
    this.authService.login(this.model).subscribe({
      next: response => {
        console.log(response.token);

        this.cookieService.set('Authentication', `${response.token}`, undefined, '/', undefined, true, 'Strict');
        this.authService.setUser({ email: this.model.email });
        this.router.navigateByUrl('/');
=======

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

    // Xóa thông báo lỗi trước khi gửi yêu cầu
    this.errorMessage = '';
    this.isLoading = true; // Bật trạng thái loading

    // Gửi yêu cầu đăng nhập
    this.authService.login(this.model).subscribe({
      next: response => {
        // Đăng nhập thành công
        this.cookieService.set('Authentication', `${response.token}`, undefined, '/', undefined, true, 'Strict');
        this.authService.setUser({ email: this.model.email });
        this.router.navigateByUrl('/');
        this.isLoading = false; // Tắt trạng thái loading
      },
      error: err => {
        // Xử lý lỗi từ server
        this.isLoading = false; // Tắt trạng thái loading
        if (err.status === 401) {
          this.errorMessage = 'Email hoặc mật khẩu không đúng';
        } else if (err.status === 400) {
          this.errorMessage = err.error.message || 'Dữ liệu không hợp lệ';
        } else {
          this.errorMessage = 'Đăng nhập thất bại. Vui lòng thử lại sau!';
        }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
      }
    });

  }

}
