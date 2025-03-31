import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MedicalRecordService } from '../../services/medical-record.service';
import { MedicalRecordCreate } from '../../models/medical-record-create.model';
import { MedicalRecordDetail } from '../../models/medical-record-detail.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
import { AppointmentService } from '../../../appointment/services/appointment.service';

@Component({
  selector: 'app-manage-medical-record',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './manage-medical-record.component.html',
  styleUrls: ['./manage-medical-record.component.css']
})
export class ManageMedicalRecordComponent implements OnInit {
  record: MedicalRecordCreate = {
    appointmentId: 0,
    diagnosis: '',
    prescription: '',
    notes: ''
  };
  medicalRecord: MedicalRecordDetail | null = null;
  appointments = [{ id: 1, doctorName: 'Dr. John Doe' }, { id: 2, doctorName: 'Dr. Jane Smith' }]; // Dữ liệu mẫu
  errorMessage: string = '';
  isDoctor: boolean = false;
  recordId: number | null = null;

  constructor(
    private medicalRecordService: MedicalRecordService,
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.user().subscribe(user => {
      const role = localStorage.getItem('role');
      this.isDoctor = role === 'Doctor';
    });

    this.recordId = +this.route.snapshot.paramMap.get('id')! || null;
    if (this.recordId) {
      this.loadMedicalRecord();
    }

    // Tải danh sách lịch hẹn từ API nếu cần
    this.appointmentService.getAppointments().subscribe({
      next: (response) => {
        this.appointments = response.data;
      }
    });
  }

  loadMedicalRecord(): void {
    if (this.recordId) {
      this.medicalRecordService.viewMedicalRecord(this.recordId).subscribe({
        next: (response) => {
          if (response && response.data) {
            this.medicalRecord = response.data;
            this.record = {
              appointmentId: this.medicalRecord!.appointmentId,
              diagnosis: this.medicalRecord!.diagnosis,
              prescription: this.medicalRecord!.prescription,
              notes: this.medicalRecord!.notes
            };
          } else {
            console.error('Không tìm thấy hồ sơ y tế.');
          }
        },
        error: (error) => {
          console.error('Lỗi khi xem hồ sơ y tế:', error);
        }
      });
    }
  }

  onSubmit(): void {
    if (this.recordId) {
      // Cập nhật hồ sơ
      this.medicalRecordService.updateMedicalRecord(this.recordId, this.record).subscribe({
        next: () => {
          alert('Cập nhật hồ sơ y tế thành công!');
          this.router.navigate(['/medical-records']);
        },
        error: (error) => {
          this.errorMessage = error.error.message || 'Đã xảy ra lỗi khi cập nhật hồ sơ y tế.';
        }
      });
    } else {
      // Thêm hồ sơ mới
      this.medicalRecordService.addMedicalRecord(this.record).subscribe({
        next: () => {
          alert('Thêm hồ sơ y tế thành công!');
          this.router.navigate(['/medical-records']);
        },
        error: (error) => {
          this.errorMessage = error.error.message || 'Đã xảy ra lỗi khi thêm hồ sơ y tế.';
        }
      });
    }
  }
}