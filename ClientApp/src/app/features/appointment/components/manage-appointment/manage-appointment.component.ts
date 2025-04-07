import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentDetail } from '../../models/appointment-detail.model';
import { Router, RouterLink } from '@angular/router';
import { MedicalRecordService } from '../../../medicalRecord/services/medical-record.service';

@Component({
  selector: 'app-manage-appointment',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './manage-appointment.component.html',
  styleUrls: ['./manage-appointment.component.css']
})
export class ManageAppointmentComponent implements OnInit {
  appointments: AppointmentDetail[] = [];
  isDoctor: boolean = false;
  isPatient: boolean = false;
  errorMessage: string = '';
  isLoading: boolean = false;
  hasMedicalRecordMap: { [key: number]: boolean } = {}; // Lưu trạng thái có medical record hay không

  constructor(
    private appointmentService: AppointmentService,
    private medicalRecordService: MedicalRecordService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const role = localStorage.getItem('user-role');
    console.log('Role from localStorage:', role);
    this.isDoctor = role === 'Doctor';
    this.isPatient = role === 'Patient';

    if (!this.isDoctor && !this.isPatient) {
      this.errorMessage = 'Vai trò không hợp lệ. Vui lòng đăng nhập lại.';
      this.router.navigate(['/login']);
      return;
    }

    this.loadAppointments();
  }

  loadAppointments(): void {
    this.isLoading = true;
    this.appointmentService.getAppointments().subscribe({
      next: (response) => {
        console.log('Appointments response:', response);
        if (response.success) {
          this.appointments = response.data;
          this.checkMedicalRecords(); // Kiểm tra medical record cho từng lịch hẹn
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading appointments:', error);
        if (error.status === 401) {
          this.errorMessage = 'Bạn cần đăng nhập để xem danh sách lịch hẹn.';
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = error.error?.message || 'Lỗi khi tải danh sách lịch hẹn.';
        }
        this.isLoading = false;
      }
    });
  }

  checkMedicalRecords(): void {
    this.appointments.forEach(appointment => {
      if (appointment.status === 'Confirmed') {
        this.medicalRecordService.viewMedicalRecordByAppointment(appointment.id).subscribe({
          next: (response) => {
            this.hasMedicalRecordMap[appointment.id] = response.success && response.data != null;
          },
          error: () => {
            this.hasMedicalRecordMap[appointment.id] = false; // Không có medical record
          }
        });
      }
    });
  }

  manage(id: number, status: number): void {
    this.isLoading = true;
    this.appointmentService.manageAppointment(id, status).subscribe({
      next: (response) => {
        console.log('Manage response:', response);
        if (response.success) {
          alert(response.message);
          this.appointments = response.data;
          this.checkMedicalRecords(); // Cập nhật lại trạng thái medical record
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error managing appointment:', error);
        if (error.status === 401) {
          this.errorMessage = 'Bạn cần đăng nhập để thực hiện hành động này.';
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = error.error?.message || 'Đã xảy ra lỗi khi cập nhật trạng thái.';
        }
        this.isLoading = false;
      }
    });
  }

  viewMedicalRecord(appointmentId: number): void {
    this.isLoading = true;
    this.medicalRecordService.viewMedicalRecordByAppointment(appointmentId).subscribe({
      next: (response) => {
        console.log('Medical record response:', response);
        if (response.success && response.data) {
          this.router.navigate(['/medical-records', response.data.id]);
        } else {
          this.errorMessage = 'Không tìm thấy hồ sơ y tế cho lịch hẹn này.';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error viewing medical record:', error);
        if (error.status === 401) {
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = 'Lỗi khi tải hồ sơ y tế.';
        }
        this.isLoading = false;
      }
    });
  }
}