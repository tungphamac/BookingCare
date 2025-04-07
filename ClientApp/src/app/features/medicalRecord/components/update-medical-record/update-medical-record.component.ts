import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { MedicalRecordService } from '../../services/medical-record.service';
import { MedicalRecordCreate } from '../../models/medical-record-create.model';
import { AppointmentService } from '../../../appointment/services/appointment.service';
import { AppointmentDetail } from '../../../appointment/models/appointment-detail.model';

@Component({
  selector: 'app-update-medical-record',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './update-medical-record.component.html',
  styleUrls: ['./update-medical-record.component.css']
})
export class UpdateMedicalRecordComponent implements OnInit {
  record: MedicalRecordCreate = {
    appointmentId: 0,
    diagnosis: '',
    prescription: '',
    notes: ''
  };
  appointment: AppointmentDetail | null = null; // Lưu thông tin lịch hẹn hiện tại
  errorMessage: string = '';
  recordId: number = 0;
  isLoading: boolean = false;

  constructor(
    private medicalRecordService: MedicalRecordService,
    private appointmentService: AppointmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const role = localStorage.getItem('user-role');
    console.log('Role from localStorage:', role);
    const isDoctor = role === 'Doctor';

    if (!isDoctor) {
      this.errorMessage = 'Chỉ bác sĩ mới có quyền cập nhật hồ sơ y tế.';
      this.router.navigate(['/appointments']);
      return;
    }

    this.recordId = +this.route.snapshot.paramMap.get('id')!;
    if (this.recordId) {
      this.loadMedicalRecord();
    } else {
      this.errorMessage = 'Không tìm thấy ID hồ sơ y tế.';
      this.router.navigate(['/appointments']);
    }
  }

  loadMedicalRecord(): void {
    this.isLoading = true;
    this.medicalRecordService.viewMedicalRecord(this.recordId).subscribe({
      next: (response) => {
        console.log('Medical record response:', response);
        if (response.success && response.data) {
          this.record = {
            appointmentId: response.data.appointmentId,
            diagnosis: response.data.diagnosis,
            prescription: response.data.prescription,
            notes: response.data.notes
          };
          this.loadAppointmentDetails(response.data.appointmentId); // Tải thông tin lịch hẹn
        } else {
          this.errorMessage = 'Không tìm thấy hồ sơ y tế.';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading medical record:', error);
        this.errorMessage = error.error?.message || 'Lỗi khi tải hồ sơ y tế.';
        this.isLoading = false;
      }
    });
  }

  loadAppointmentDetails(appointmentId: number): void {
    this.appointmentService.getAppointmentDetail(appointmentId).subscribe({
      next: (response) => {
        console.log('Appointment detail response:', response);
        if (response.success && response.data) {
          this.appointment = response.data;
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading appointment:', error);
        this.errorMessage = 'Lỗi khi tải thông tin lịch hẹn: ' + (error.error?.message || error.message);
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.recordId === 0) {
      this.errorMessage = 'ID hồ sơ y tế không hợp lệ.';
      return;
    }
    this.isLoading = true;
    this.medicalRecordService.updateMedicalRecord(this.recordId, this.record).subscribe({
      next: () => {
        alert('Cập nhật hồ sơ y tế thành công!');
        this.router.navigate(['/appointments']);
      },
      error: (error) => {
        console.error('Error updating medical record:', error);
        this.errorMessage = error.error?.message || 'Đã xảy ra lỗi khi cập nhật hồ sơ y tế.';
        this.isLoading = false;
      }
    });
  }
}