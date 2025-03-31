import { Component } from '@angular/core';
import { RegisterVm } from '../Models/register.model';
import { AuthService } from '../../auth/services/auth.service';
import { FormBuilder, FormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
<<<<<<< HEAD
=======
import { Route } from '@angular/router';
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
<<<<<<< HEAD
=======

>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
})
export class RegisterComponent {
  model: RegisterVm;
  message: string = '';


<<<<<<< HEAD
  constructor(private fb: FormBuilder, private authService: AuthService) {
=======
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
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
<<<<<<< HEAD
        console.log(" Đăng ký thành công", response);
        alert("Đăng ký thành công!"); // Hoặc chuyển hướng sang trang đăng nhập
      },
      error: (error) => {
        console.error(" Lỗi đăng ký:", error.message);
        alert(error.message); // Hiển thị lỗi từ API
      }
    });
  }

=======
        console.log("Đăng ký thành công", response);
        alert("Đăng ký thành công!");
        this.router.navigate(['/login']); // Chuyển hướng về trang đăng nhập
      },
      error: (error) => {
        console.error("Lỗi đăng ký:", error);
        alert(error.message); // Hiển thị lỗi chi tiết từ API
      }
    });
  }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
}
