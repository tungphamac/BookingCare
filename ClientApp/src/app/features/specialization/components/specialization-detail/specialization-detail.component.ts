import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SpecializationService } from '../../services/specialization.service';
import { ClinicService } from '../../../clinic/services/clinic.service';
import { Specialization } from '../../models/specialization.model';
import { RouterModule } from '@angular/router';
import { ClinicDetailDto } from '../../../clinic/models/clinic-detail.model';
import { DoctorDetailDto } from '../../../doctor/models/doctor-detail.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-specialization-detail',
  templateUrl: './specialization-detail.component.html',
  imports: [CommonModule, RouterModule, FormsModule],
  styleUrls: ['./specialization-detail.component.css'],
  standalone: true
})
export class SpecializationDetailComponent implements OnInit {
  specialization: Specialization | null = null;
  doctors: DoctorDetailDto[] = [];
  clinics: ClinicDetailDto[] = [];
  errorMessage: string | null = null;
  isLoading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private specializationService: SpecializationService,
    private clinicService: ClinicService
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const specializationId = idParam ? Number(idParam) : null;

    if (specializationId !== null && !isNaN(specializationId)) {
      this.loadSpecializationDetails(specializationId);
    } else {
      this.errorMessage = 'ID chuyên khoa không hợp lệ.';
    }
  }

  loadSpecializationDetails(specializationId: number): void {
    this.isLoading = true;

    this.specializationService.getSpecializationById(specializationId).subscribe({
      next: (data) => {
        this.specialization = data;
        this.loadClinicsBySpecialization(specializationId);
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải thông tin chuyên khoa.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  loadClinicsBySpecialization(specializationId: number): void {
    this.clinicService.getClinicBySpecializationId(specializationId).subscribe({
      next: (data) => {
        this.clinics = data;
        this.loadDoctorsBySpecialization(specializationId);
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải danh sách phòng khám.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  loadDoctorsBySpecialization(specializationId: number): void {
    this.specializationService.getDoctorsBySpecializationId(specializationId).subscribe({
      next: (data) => {
        this.doctors = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải danh sách bác sĩ.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }
}