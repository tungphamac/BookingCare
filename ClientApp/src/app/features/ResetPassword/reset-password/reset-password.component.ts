import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { resetPasswordVm } from '../Models/resetPass.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reset-password',
  imports: [FormsModule, RouterModule, CommonModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit {
  model: resetPasswordVm = {
    Email: '',
    NewPassword: '',
    ConfirmNewPassword: '',
    Token: ''
  };
  errorMessage: string = '';

  constructor(private authService: AuthService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.model.Email = this.route.snapshot.queryParams['email'];
    this.model.Token = this.route.snapshot.queryParams['token'];
  }

  onFormSubmit() {
    if (!this.model.NewPassword || !this.model.ConfirmNewPassword) {
      this.errorMessage = 'Mật khẩu mới và xác nhận mật khẩu là bắt buộc';
      return;
    }
    if (this.model.NewPassword !== this.model.ConfirmNewPassword) {
      this.errorMessage = 'Mật khẩu xác nhận không khớp';
      return;
    }
    if (this.model.NewPassword.length < 6) {
      this.errorMessage = 'Mật khẩu phải có ít nhất 6 ký tự';
      return;
    }

    this.errorMessage = '';
    this.authService.resetPassword(this.model).subscribe({
      next: (res) => {
        this.errorMessage = 'Mật khẩu đã được đặt lại thành công!';
      },
      error: (err) => {
        // Hiển thị lỗi chi tiết từ server
        if (err.error.errors) {
          // Lỗi từ ModelState
          const errors = Object.values(err.error.errors).flat().join(', ');
          this.errorMessage = errors;
        } else if (err.error.message) {
          // Lỗi từ ResetPasswordAsync
          const errors = err.error.errors ? err.error.errors.join(', ') : '';
          this.errorMessage = `${err.error.message}${errors ? ': ' + errors : ''}`;
        } else {
          this.errorMessage = 'Có lỗi xảy ra!';
        }
      }
    });
  }
}
