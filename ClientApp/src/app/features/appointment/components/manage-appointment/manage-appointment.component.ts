import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentDetail } from '../../models/appointment-detail.model';
import { AppointmentStatus } from '../../models/appointment-status.enum';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-manage-appointment',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-appointment.component.html',
  styleUrls: ['./manage-appointment.component.css']
})
export class ManageAppointmentComponent implements OnInit {
  appointments: AppointmentDetail[] = [];
  isDoctor: boolean = false;
  isPatient: boolean = false;
  isLoading: boolean = false;

  constructor(private appointmentService: AppointmentService, private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.user().subscribe(user => {
      const role = localStorage.getItem('role');
      this.isDoctor = role === 'Doctor';
      this.isPatient = role === 'Patient';
    });

    this.loadAppointments();
  }

  loadAppointments(): void {
    this.appointmentService.getAppointments().subscribe({
      next: (response) => {
        this.appointments = response.data;
      },
      error: (error) => {
        console.error('Lỗi khi tải danh sách lịch hẹn:', error);
      }
    });
  }

  manage(id: number, status: number): void {
    this.isLoading = true;
    this.appointmentService.manageAppointment(id, status, 1, 10).subscribe({
      next: (response) => {
        alert(response.message); // Hiển thị thông báo
        this.appointments = response.data; // Cập nhật danh sách lịch hẹn từ response
      },
      error: (error) => {
        alert(error.error?.message || 'Đã xảy ra lỗi khi cập nhật trạng thái.');
      }
    });
  }
}