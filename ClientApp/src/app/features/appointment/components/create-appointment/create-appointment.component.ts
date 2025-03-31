import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentCreate } from '../../models/appointment-create.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-appointment',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './create-appointment.component.html',
  styleUrls: ['./create-appointment.component.css']
})
export class CreateAppointmentComponent implements OnInit {
  appointment: AppointmentCreate = {
    date: new Date(),
    time: '',
    reason: '',
    doctorId: 0,
    scheduleId: 0,
    clinicId: 0
  };
  doctors = [{ id: 2, name: 'Dr. John Doe' }, { id: 3, name: 'Dr. Jane Smith' }]; // Dữ liệu mẫu
  schedules = [{ id: 1, time: '10:00 AM' }, { id: 2, time: '11:00 AM' }]; // Dữ liệu mẫu
  clinics = [{ id: 1, name: 'Phòng khám A' }, { id: 2, name: 'Phòng khám B' }]; // Dữ liệu mẫu
  errorMessage: string = '';

  constructor(private appointmentService: AppointmentService, private router: Router) {}

  ngOnInit(): void {
    // Tải danh sách bác sĩ, lịch trống, phòng khám từ API nếu cần
  }

  onSubmit(): void {
    this.appointmentService.createAppointment(this.appointment).subscribe({
      next: (response) => {
        alert('Tạo lịch hẹn thành công!');
        this.router.navigate(['/appointments']);
      },
      error: (error) => {
        this.errorMessage = error.error.message || 'Đã xảy ra lỗi khi tạo lịch hẹn.';
      }
    });
  }
}