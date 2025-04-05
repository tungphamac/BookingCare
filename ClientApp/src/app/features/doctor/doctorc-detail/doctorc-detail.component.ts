import { GetDoctor } from './../models/doctor.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DoctorService } from '../services/doctor.service';
import { Doctor } from '../models/doctor.model';
import { Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ScheduleService } from '../../schedule/services/schedule.service';
import { Schedule } from '../../schedule/models/schedule.model';

@Component({
  selector: 'app-doctor-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './doctorc-detail.component.html',
  styleUrl: './doctorc-detail.component.css'
})
export class DoctorDetailComponent implements OnInit {
  doctor: Doctor | null = null;
  schedules: Schedule[] = [];
  errorMessage: string | null = null;
  isLoadingSchedules: boolean = false;
  GetDoctor: GetDoctor | null = null;

  constructor(
    private route: ActivatedRoute,
    private doctorService: DoctorService,
    private scheduleService: ScheduleService,
    private location: Location,
    private Router: Router
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;
    if (id !== null && !isNaN(id)) {
      this.loadDoctorDetails(id);
      this.loadDoctorSchedules(id);
    } else {
      this.errorMessage = 'Invalid doctor ID.';
    }
  }

  loadDoctorDetails(doctorId: number): void {
    this.doctorService.getDoctorById(doctorId).subscribe({
      next: (data: Doctor) => {
        this.doctor = data;
      },
      error: (err) => {
        if (err.status === 404) {
          this.errorMessage = 'Doctor not found.';
        } else {
          this.errorMessage = 'Failed to load doctor details. Please try again later.';
        }
        console.error(err);
      }
    });
  }

  loadDoctorSchedules(id: number): void {
    this.isLoadingSchedules = true;
    this.scheduleService.getSchedulesByDoctorId(id).subscribe({
      next: (schedules) => {
        this.schedules = schedules;
        this.isLoadingSchedules = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load doctor schedules.';
        this.isLoadingSchedules = false;
        console.error(err);
      }
    });
  }

  navigateToCreateAppointment(): void {
    if (this.doctor) {
      console.log('Doctor data:', this.doctor);
      if (!this.doctor.clinicId) {
        console.error('ClinicId is missing in doctor data');
        this.errorMessage = 'Không thể tạo lịch hẹn do thiếu thông tin phòng khám.';
        return;
      }
      this.Router.navigate(['/appointments/create'], {
        queryParams: {
          doctorId: this.doctor.id,
          clinicId: this.doctor.clinicId
        }
      });
    } else {
      console.error('Doctor is null');
      this.errorMessage = 'Không có thông tin bác sĩ để tạo lịch hẹn.';
    }
  }

  goBack(): void {
    this.location.back();
  }
  
}