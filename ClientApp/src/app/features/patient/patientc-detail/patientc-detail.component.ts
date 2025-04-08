import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientService } from '../services/patient.service';
import { Patient } from '../models/patient';
import { Location } from '@angular/common';
import { PatientDetail } from '../models/patient-detail.model';
import { FormsModule } from '@angular/forms';
import { ChangePasswordVm } from '../models/changepassword.model';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-patient-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './patientc-detail.component.html',
  styleUrl: './patientc-detail.component.css'
})
export class PatientDetailComponent implements OnInit {
  patient: PatientDetail | null = null;
  errorMessage: string | null = null;
  isEditing: boolean = false;
  showPasswordForm: boolean = false; // Điều khiển hiển thị form đổi mật khẩu
  changePasswordVm: ChangePasswordVm = {
    OldPassword: '',
    NewPassword: '',
    ConfirmNewPassword: ''
  };

  constructor(
    private route: ActivatedRoute,
    private patientService: PatientService,
    private location: Location,
    private authService: AuthService // Thêm AuthService

  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;
    if (id !== null && !isNaN(id)) {
      this.patientService.getPatientDetail(id).subscribe({
        next: (data: PatientDetail) => {
          this.patient = data;
        },
        error: (err) => {
          if (err.status === 404) {
            this.errorMessage = 'Patient not found.';
          } else if (err.status === 401) {
            this.errorMessage = 'You are not authorized to view this patient\'s details.';
          } else {
            this.errorMessage = 'Failed to load patient details. Please try again later.';
          }
          console.error(err);
        }
      });
    } else {
      this.errorMessage = 'Invalid patient ID.';
    }
  }

  goBack(): void {
    this.location.back();
  }
  changePassword(): void {
    this.showPasswordForm = true; // Hiển thị form đổi mật khẩu
  }
  submitPasswordChange(): void {
    if (!this.changePasswordVm.OldPassword) {
      this.errorMessage = 'Vui lòng nhập mật khẩu cũ!';
      return;
    }
    if (this.changePasswordVm.NewPassword !== this.changePasswordVm.ConfirmNewPassword) {
      this.errorMessage = 'Mật khẩu xác nhận không khớp!';
      return;
    }

    this.patientService.changePassword(this.changePasswordVm).subscribe({
      next: () => {
        alert('Đổi mật khẩu thành công!');
        this.showPasswordForm = false;
        this.resetPasswordForm();
      },
      error: (err) => {
        if (err.status === 401) {
          this.errorMessage = 'Không được phép! Vui lòng đăng nhập lại.';
        } else if (err.status === 400) {
          this.errorMessage = 'Mật khẩu cũ không đúng hoặc dữ liệu không hợp lệ.';
        } else {
          this.errorMessage = 'Đổi mật khẩu thất bại. Vui lòng thử lại sau.';
        }
        console.error('Lỗi đổi mật khẩu:', err);
      }
    });
  }
  cancelPasswordChange(): void {
    this.showPasswordForm = false;
    this.resetPasswordForm();
  }
  private resetPasswordForm(): void {
    this.changePasswordVm = {
      OldPassword: '',
      NewPassword: '',
      ConfirmNewPassword: ''
    };
    this.errorMessage = null;
  }
}
