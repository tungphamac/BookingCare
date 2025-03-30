import { Component } from '@angular/core';
import { RegisterVm } from '../Models/register.model';
import { AuthService } from '../../auth/services/auth.service';
import { FormBuilder, FormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  model: RegisterVm;
  message: string = '';


  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.model = {
      UserName: "",
      Email: "",
      Password: "",
      Gender: true,  // Mặc định là Nam (true)
      Phone: "",
      Address: "",
      Avatar: "",
      MedicalHistory: ""
    };
  }
  onFormSubmit() {
    console.log("Dữ liệu gửi đi:", this.model);
    this.authService.register(this.model).subscribe({
      next: (response) => {
        console.log("✅ Đăng ký thành công", response);
        alert("Đăng ký thành công!"); // Hoặc chuyển hướng sang trang đăng nhập
      },
      error: (error) => {
        console.error("❌ Lỗi đăng ký:", error.message);
        alert(error.message); // Hiển thị lỗi từ API
      }
    });
  }

}
