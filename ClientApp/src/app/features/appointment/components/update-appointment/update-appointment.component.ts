import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentCreate } from '../../models/appointment-create.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ScheduleService } from '../../../schedule/services/schedule.service';
import { Schedule } from '../../../schedule/models/schedule.model';

@Component({
  selector: 'app-update-appointment',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './update-appointment.component.html',
  styleUrls: ['./update-appointment.component.css']
})
export class UpdateAppointmentComponent implements OnInit {
  appointment: AppointmentCreate = {
    date: new Date(),
    time: '',
    reason: '',
    doctorId: 0,
    scheduleId: 0,
    clinicId: 0
  };
  schedules: Schedule[] = [];
  errorMessage: string = '';
  appointmentId: number = 0;

  constructor(
    private appointmentService: AppointmentService,
    private scheduleService: ScheduleService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.appointmentId = +this.route.snapshot.paramMap.get('id')!;
    if (this.appointmentId) {
      this.loadAppointmentDetails();
    } else {
      this.errorMessage = 'Không tìm thấy ID lịch hẹn.';
      this.router.navigate(['/appointments']);
    }
  }

  loadAppointmentDetails(): void {
    this.appointmentService.getAppointmentDetail(this.appointmentId).subscribe({
      next: (response) => {
        const appointmentDto = response.data; // Truy cập 'data' từ phản hồi
        this.appointment = {
          date: new Date(appointmentDto.scheduleTime),
          time: this.formatTime(appointmentDto.scheduleTime), // Chuyển ScheduleTime thành time string
          reason: appointmentDto.reason,
          doctorId: appointmentDto.doctorId,
          scheduleId: appointmentDto.scheduleId,
          clinicId: appointmentDto.clinicId
        };
        console.log('Appointment loaded:', this.appointment);
        this.loadSchedules(appointmentDto.doctorId); // Tải danh sách lịch
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải thông tin lịch hẹn: ' + (err.statusText || 'Lỗi không xác định');
        console.error(err);
        this.router.navigate(['/appointments']);
      }
    });
  }

  // Hàm chuyển đổi ScheduleTime thành định dạng time (HH:mm)
  private formatTime(scheduleTime: string): string {
    const date = new Date(scheduleTime);
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
  }

  loadSchedules(doctorId: number): void {
    this.scheduleService.getSchedulesByDoctorId(doctorId).subscribe({
      next: (schedules) => {
        const currentSchedule = schedules.find(s => s.id === this.appointment.scheduleId);
        const availableSchedules = schedules.filter(s => s.status === 'Available');
        
        this.schedules = [
          ...(currentSchedule ? [currentSchedule] : []),
          ...availableSchedules.filter(s => s.id !== this.appointment.scheduleId)
        ];
        
        console.log('Schedules loaded:', this.schedules);
        if (this.schedules.length === 0) {
          this.errorMessage = '';
        }
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải danh sách lịch: ' + (err.statusText || 'Lỗi không xác định');
        console.error(err);
      }
    });
  }

  onScheduleChange(event: Event): void {
    const selectedScheduleId = +(event.target as HTMLSelectElement).value;
    const selectedSchedule = this.schedules.find(s => s.id === selectedScheduleId);
    if (selectedSchedule) {
      this.appointment.scheduleId = selectedSchedule.id;
      this.appointment.time = selectedSchedule.timeSlot;
      this.appointment.date = new Date(selectedSchedule.workDate);
    }
  }

  onSubmit(): void {
    if (!this.appointment.scheduleId) {
      this.errorMessage = 'Vui lòng chọn một lịch.';
      return;
    }
    const token = localStorage.getItem('token');
    if (!token) {
      this.errorMessage = 'Vui lòng đăng nhập để cập nhật lịch hẹn.';
      this.router.navigate(['/login']);
      return;
    }

    this.appointmentService.updateAppointment(this.appointmentId, this.appointment).subscribe({
      next: () => {
        alert('Cập nhật lịch hẹn thành công!');
        this.router.navigate(['/appointments']);
      },
      error: (err) => {
        const errorMessage = err.error && err.error.message ? err.error.message : err.statusText || 'Lỗi không xác định';
        this.errorMessage = 'Không thể cập nhật lịch hẹn: ' + errorMessage;
        console.error('Error details:', err);
      }
    });
  }
}