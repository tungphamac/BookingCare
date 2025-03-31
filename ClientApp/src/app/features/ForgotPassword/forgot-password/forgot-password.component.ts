import { Component } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { FormsModule } from '@angular/forms';
import { forgotPasswordVm } from '../Models/forgot.model';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  model: forgotPasswordVm = { Email: '' };
  message: string = '';
  errorMessage: string = '';
  constructor(private authService: AuthService, private router: Router) {

  }

  onFormSubmit() {
    // Validation phía client
    if (!this.model.Email) {
      this.errorMessage = 'Email là bắt buộc';
      return;
    }
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(this.model.Email)) {
      this.errorMessage = 'Email không hợp lệ';
      return;
    }

    this.errorMessage = ''; // Xóa thông báo lỗi nếu validation thành công
    this.authService.forgotPassword(this.model).subscribe({
      next: (res) => {
        this.message = "Email đặt lại mật khẩu đã được gửi!";
      },
      error: (err) => {
        this.message = err.error.message || "Có lỗi xảy ra!";
      }
    });
  }
}
