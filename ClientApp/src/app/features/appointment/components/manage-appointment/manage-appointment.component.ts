import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentDetail } from '../../models/appointment-detail.model';
import { AppointmentStatus } from '../../models/appointment-status.enum';
import { AuthService } from '../../../auth/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { MedicalRecordService } from '../../../medicalRecord/services/medical-record.service';

@Component({
  selector: 'app-manage-appointment',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './manage-appointment.component.html',
  styleUrls: ['./manage-appointment.component.css']
})
export class ManageAppointmentComponent implements OnInit {
  appointments: AppointmentDetail[] = [];
  // isDoctor: boolean = false;
  // isPatient: boolean = false;
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private appointmentService: AppointmentService,
    private MedicalRecordService: MedicalRecordService, 
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // this.authService.user().subscribe(user => {
    //   const role = localStorage.getItem('role');
    //   this.isDoctor = role === 'Doctor';
    //   this.isPatient = role === 'Patient';
    // });

    this.loadAppointments();
  }

  loadAppointments(): void {
    this.isLoading = true;
    this.appointmentService.getAppointments().subscribe({
      next: (response) => {
        if (response.success) {
          this.appointments = response.data;
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 401 || error.message === 'No authentication token found. Please log in.') {
          this.errorMessage = 'Bạn cần đăng nhập để xem danh sách lịch hẹn.';
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = error.error?.message || 'Lỗi khi tải danh sách lịch hẹn.';
        }
        this.isLoading = false;
      }
    });
  }

  manage(id: number, status: number): void {
    this.isLoading = true;
    this.appointmentService.manageAppointment(id, status).subscribe({
      next: (response) => {
        if (response.success) {
          alert(response.message);
          this.appointments = response.data;
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 401 || error.message === 'No authentication token found. Please log in.') {
          this.errorMessage = 'Bạn cần đăng nhập để thực hiện hành động này.';
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = error.error?.message || 'Đã xảy ra lỗi khi cập nhật trạng thái.';
        }
        this.isLoading = false;
      }
    });
  }

  viewMedicalRecord(appointmentId: number): void {
    this.isLoading = true;
    // Giả sử cần tìm medicalRecordId từ appointmentId (cần backend hỗ trợ hoặc dữ liệu bổ sung)
    this.MedicalRecordService.viewMedicalRecordByAppointment(appointmentId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.router.navigate(['/medical-records', response.data.id]);
        } else {
          this.errorMessage = 'Không tìm thấy hồ sơ y tế cho lịch hẹn này.';
        }
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 401) {
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = 'Lỗi khi tải hồ sơ y tế.';
        }
        this.isLoading = false;
      }
    });
  }

}