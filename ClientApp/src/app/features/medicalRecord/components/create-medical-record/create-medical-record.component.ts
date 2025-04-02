import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { MedicalRecordService } from '../../services/medical-record.service';
import { MedicalRecordCreate } from '../../models/medical-record-create.model';
import { AuthService } from '../../../auth/services/auth.service';
import { AppointmentService } from '../../../appointment/services/appointment.service';
import { AppointmentDetail } from '../../../appointment/models/appointment-detail.model';

@Component({
  selector: 'app-create-medical-record',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './create-medical-record.component.html',
  styleUrls: ['./create-medical-record.component.css']
})
export class CreateMedicalRecordComponent implements OnInit {
  record: MedicalRecordCreate = {
    appointmentId: 0,
    diagnosis: '',
    prescription: '',
    notes: ''
  };
  appointments: AppointmentDetail[] = [];
  errorMessage: string = '';
  // isDoctor: boolean = false;
  isLoading: boolean = false;

  constructor(
    private medicalRecordService: MedicalRecordService,
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // this.authService.user().subscribe(user => {
    //   const role = localStorage.getItem('role');
    //   this.isDoctor = role === 'Doctor';
    //   if (!this.isDoctor) {
    //     this.errorMessage = 'Chỉ bác sĩ mới có quyền tạo hồ sơ y tế.';
    //     this.router.navigate(['/appointments']);
    //   } else {
        this.loadAppointments();
    //   }
    // });
  }

  loadAppointments(): void {
    this.isLoading = true;
    this.appointmentService.getAppointments().subscribe({
      next: (response) => {
        this.appointments = response.data.filter(appointment => appointment.status === 'Confirmed'); // Chỉ lấy lịch hẹn đã xác nhận
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 401) {
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = 'Lỗi khi tải danh sách lịch hẹn: ' + (error.error?.message || error.message);
        }
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    // if (!this.isDoctor) return;
    this.isLoading = true;
    this.medicalRecordService.addMedicalRecord(this.record).subscribe({
      next: () => {
        alert('Thêm hồ sơ y tế thành công!');
        this.router.navigate(['/medical-records']);
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Đã xảy ra lỗi khi thêm hồ sơ y tế.';
        this.isLoading = false;
      }
    });
  }
}