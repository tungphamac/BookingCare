import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { MedicalRecordService } from '../../services/medical-record.service';
import { MedicalRecordCreate } from '../../models/medical-record-create.model';
import { MedicalRecordDetail } from '../../models/medical-record-detail.model';
import { AuthService } from '../../../auth/services/auth.service';
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
  appointments: AppointmentDetail[] = [];
  errorMessage: string = '';
  // isDoctor: boolean = false;
  recordId: number = 0;
  isLoading: boolean = false;

  constructor(
    private medicalRecordService: MedicalRecordService,
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // this.authService.user().subscribe(user => {
    //   const role = localStorage.getItem('role');
    //   this.isDoctor = role === 'Doctor';
    //   if (!this.isDoctor) {
    //     this.errorMessage = 'Chỉ bác sĩ mới có quyền cập nhật hồ sơ y tế.';
    //     this.router.navigate(['/appointments']);
    //   } else {
        this.recordId = +this.route.snapshot.paramMap.get('id')!;
        if (this.recordId) {
          this.loadMedicalRecord();
          this.loadAppointments();
        } else {
          this.errorMessage = 'Không tìm thấy ID hồ sơ y tế.';
          this.router.navigate(['/medical-records']);
        }
      }
  //   });
  // }

  loadAppointments(): void {
    this.isLoading = true;
    this.appointmentService.getAppointments().subscribe({
      next: (response) => {
        this.appointments = response.data.filter(appointment => appointment.status === 'Confirmed');
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Lỗi khi tải danh sách lịch hẹn: ' + (error.error?.message || error.message);
        this.isLoading = false;
      }
    });
  }

  loadMedicalRecord(): void {
    this.isLoading = true;
    this.medicalRecordService.viewMedicalRecord(this.recordId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.record = {
            appointmentId: response.data.appointmentId,
            diagnosis: response.data.diagnosis,
            prescription: response.data.prescription,
            notes: response.data.notes
          };
        } else {
          this.errorMessage = 'Không tìm thấy hồ sơ y tế.';
        }
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Lỗi khi tải hồ sơ y tế.';
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    // if (!this.isDoctor || !this.recordId) return;
    this.isLoading = true;
    this.medicalRecordService.updateMedicalRecord(this.recordId, this.record).subscribe({
      next: () => {
        alert('Cập nhật hồ sơ y tế thành công!');
        this.router.navigate(['/medical-records']);
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Đã xảy ra lỗi khi cập nhật hồ sơ y tế.';
        this.isLoading = false;
      }
    });
  }
}