import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicalRecordService } from '../../services/medical-record.service';
import { MedicalRecordDetail } from '../../models/medical-record-detail.model';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-view-medical-record',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-medical-record.component.html',
  styleUrls: ['./view-medical-record.component.css']
})
export class ViewMedicalRecordComponent implements OnInit {
  medicalRecord: MedicalRecordDetail | null = null;
  errorMessage: string = '';
  isLoading: boolean = false;
  // isDoctor: boolean = false;
  // isPatient: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private medicalRecordService: MedicalRecordService,
    // private authService: AuthService
  ) {}

  ngOnInit(): void {
    // this.authService.user().subscribe(user => {
    //   const role = localStorage.getItem('role');
    //   this.isDoctor = role === 'Doctor';
    //   this.isPatient = role === 'Patient';
    //   if (!this.isDoctor && !this.isPatient) {
    //     this.errorMessage = 'Bạn không có quyền xem hồ sơ y tế.';
    //     this.router.navigate(['/login']);
    //   } else {
        const recordId = Number(this.route.snapshot.paramMap.get('id'));
        if (isNaN(recordId)) {
          this.errorMessage = 'ID hồ sơ y tế không hợp lệ.';
          this.router.navigate(['/appointments']);
        } else {
          this.loadMedicalRecord(recordId);
        }
    //   }
    // });
  }

  loadMedicalRecord(recordId: number): void {
    this.isLoading = true;
    this.medicalRecordService.viewMedicalRecord(recordId).subscribe({
      next: (response) => {
        if (response.success) {
          this.medicalRecord = response.data;
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
      },
      error: (error) => {
        if (error.status === 401) {
          this.errorMessage = 'Bạn cần đăng nhập để xem hồ sơ y tế.';
          this.router.navigate(['/login']);
        } else if (error.status === 404) {
          this.errorMessage = 'Không tìm thấy hồ sơ y tế.';
        } else {
          this.errorMessage = error.error?.message || 'Lỗi khi tải hồ sơ y tế.';
        }
        this.isLoading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/appointments']);
  }
}