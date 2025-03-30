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
    ConfirmPassword: '',
    Token: ''
  };
  message: string = '';

  constructor(private authService: AuthService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.model.Email = this.route.snapshot.queryParams['email'];
    this.model.Token = this.route.snapshot.queryParams['token'];
  }

  onFormSubmit() {
    this.authService.resetPassword(this.model).subscribe({
      next: (res) => {
        this.message = "Mật khẩu đã được đặt lại thành công!";
      },
      error: (err) => {
        this.message = err.error.message || "Có lỗi xảy ra!";
      }
    });
  }
}
