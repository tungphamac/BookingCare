import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { MedicalRecordService } from '../../../medicalRecord/services/medical-record.service';
import { AppointmentDetail } from '../../models/appointment-detail.model';
import { MedicalRecordDetail } from '../../../medicalRecord/models/medical-record-detail.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-view-appointment-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-appointment-detail.component.html',
  styleUrls: ['./view-appointment-detail.component.css']
})
export class ViewAppointmentDetailComponent implements OnInit {
  appointment: AppointmentDetail | null = null;
  medicalRecord: MedicalRecordDetail | null = null;
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private appointmentService: AppointmentService,
    private medicalRecordService: MedicalRecordService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    if (isNaN(id)) {
      this.errorMessage = 'ID lịch hẹn không hợp lệ.';
      this.router.navigate(['/appointments']);
      return;
    }
    this.loadAppointment(id);
  }

  loadAppointment(id: number): void {
    this.isLoading = true;
    this.appointmentService.getAppointmentDetail(id).subscribe({
      next: (response) => {
        this.appointment = response.data;
        if (this.appointment && (this.appointment as any).medicalRecordId) {
          this.loadMedicalRecord((this.appointment as any).medicalRecordId);
        } else {
          this.isLoading = false;
        }
      },
      error: (error) => {
        if (error.status === 401) {
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = error.error?.message || 'Lỗi khi tải chi tiết lịch hẹn.';
        }
        this.isLoading = false;
      }
    });
  }

  loadMedicalRecord(appointmentId: number): void {
    this.medicalRecordService.viewMedicalRecordByAppointment(appointmentId).subscribe({
      next: (response) => {
        if (response.success) {
          this.medicalRecord = response.data;
        }
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 401) {
          this.router.navigate(['/login']);
        } else if (error.status === 404) {
          this.medicalRecord = null;
        } else {
          this.errorMessage = error.error?.message || 'Lỗi khi tải hồ sơ y tế.';
        }
        this.isLoading = false;
      }
    });
  }
}