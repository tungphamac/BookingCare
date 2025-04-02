import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentCreate } from '../../models/appointment-create.model';
import { Router } from '@angular/router';
import { DoctorService } from '../../../doctor/services/doctor.service';
import { ScheduleService } from '../../../schedule/services/schedule.service';
import { ClinicService } from '../../../clinic/services/clinic.service';
import { TopDoctor } from '../../../doctor/models/top-doctor.model';
import { TopClinic } from '../../../clinic/models/top-clinic.model';

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
  doctors: TopDoctor[] = [];
  // schedules: Schedule[] = [];
  clinics: TopClinic[] = [];
  errorMessage: string = '';

  constructor(
    private appointmentService: AppointmentService,
    private doctorService: DoctorService,
    private scheduleService: ScheduleService,
    private clinicService: ClinicService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Tải danh sách bác sĩ
    this.loadDoctors();

    // Tải danh sách phòng khám
    this.loadClinics();
  }

  // Tải danh sách bác sĩ
  loadDoctors(): void {
    this.doctorService.getTopDoctors().subscribe({
      next: (doctors) => {
        this.doctors = doctors;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách bác sĩ: ' + (error.error?.message || error.message);
      }
    });
  }

  // Tải danh sách lịch trống khi bác sĩ được chọn
  // onDoctorChange(): void {
  //   if (this.appointment.doctorId) {
  //     this.scheduleService.getSchedules(this.appointment.doctorId).subscribe({
  //       next: (schedules) => {
  //         this.schedules = schedules;
  //       },
  //       error: (error) => {
  //         this.errorMessage = 'Lỗi khi tải danh sách lịch trống: ' + (error.error?.message || error.message);
  //       }
  //     });
  //   } else {
  //     this.schedules = [];
  //   }
  // }

  // Tải danh sách phòng khám
  loadClinics(): void {
    this.clinicService.getTopClinics().subscribe({
      next: (clinics) => {
        this.clinics = clinics;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách phòng khám: ' + (error.error?.message || error.message);
      }
    });
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