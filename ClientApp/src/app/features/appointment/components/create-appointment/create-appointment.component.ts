import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentCreate } from '../../models/appointment-create.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ScheduleService } from '../../../schedule/services/schedule.service';
import { DoctorService } from '../../../doctor/services/doctor.service';
import { Schedule } from '../../../schedule/models/schedule.model';
import { Doctor } from '../../../doctor/models/doctor.model';

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
  doctor: Doctor | null = null;
  schedules: Schedule[] = [];
  clinic: { id: number; name: string }| null = null;
  errorMessage: string = '';
  isDoctorDisabled: boolean = true; // Để vô hiệu hóa trường doctor
  isClinicDisabled: boolean = true; // Để vô hiệu hóa trường clinic

  constructor(
    private appointmentService: AppointmentService, 
    private router: Router,
    private route: ActivatedRoute,
    private scheduleService: ScheduleService, // Thêm ScheduleService
    private doctorService: DoctorService) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const doctorId = +params['doctorId'];
      const clinicId = +params['clinicId'];
      console.log('Query params: ', {doctorId, clinicId});

      if (doctorId && clinicId) {
        this.appointment.doctorId = doctorId;
        this.appointment.clinicId = clinicId;
        this.loadDoctorDetails(doctorId);
        this.loadSchedules(doctorId);
        this.loadClinicDetails(clinicId);
      } else {
        this.errorMessage = 'Thiếu thông tin bác sĩ hoặc phòng khám.';
        this.router.navigate(['']);
      }
    });
  }

  loadDoctorDetails(doctorId: number): void {
    this.doctorService.getDoctorById(doctorId).subscribe({
      next: (doctor) => {
        this.doctor = doctor;
        console.log('Doctor loaded:', this.doctor);
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải thông tin bác sĩ.';
      }
    });
  }

  loadSchedules(doctorId: number): void {
    this.scheduleService.getSchedulesByDoctorId(doctorId).subscribe({
      next: (schedules) => {
        // Lọc chỉ các lịch Available
        this.schedules = schedules.filter(schedule => schedule.status === 'Available');
        if (this.schedules.length > 0) {
          this.appointment.scheduleId = this.schedules[0].id; // Chọn mặc định lịch đầu tiên
          this.appointment.time = this.schedules[0].timeSlot; // Điền sẵn giờ
          this.appointment.date = new Date(this.schedules[0].workDate); // Điền sẵn ngày
        }
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải lịch trống của bác sĩ.';
      }
    });
  }

  loadClinicDetails(clinicId: number): void {
    this.doctorService.getClinicById(clinicId).subscribe({
      next: (clinic) => {
        this.clinic = clinic;
        console.log('Clinic loaded:', this.clinic);
      },
      error: (err) => {
        this.clinic = { id: clinicId, name: 'Phòng khám không xác định' };
        this.errorMessage = 'Không thể tải thông tin phòng khám: ' + (err.statusText || 'Lỗi không xác định');
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
      this.errorMessage = 'Vui lòng chọn một lịch trống.';
      return;
    }
  
    const token = localStorage.getItem('token');
    console.log('Submitting with token:', token); // Log token
    console.log('Appointment data:', this.appointment); // Log dữ liệu gửi đi
    if (!token) {
      this.errorMessage = 'Vui lòng đăng nhập để tạo lịch hẹn.';
      this.router.navigate(['/login']);
      return;
    }
  
    this.appointmentService.createAppointment(this.appointment).subscribe({
      next: () => {
        alert('Tạo lịch hẹn thành công!');
        this.router.navigate(['/appointments']);
      },
      error: (err) => {
        const errorMessage = err.error && err.error.message ? err.error.message : err.statusText || 'Lỗi không xác định';
        this.errorMessage = 'Không thể tạo lịch hẹn: ' + errorMessage;
        console.error('Error details:', err);
      }
    });
  }
}