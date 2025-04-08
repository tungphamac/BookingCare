// src/app/features/clinic/clinic-detail/clinic-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ClinicService } from '../services/clinic.service';
import { Clinic } from '../models/clinic.model';
import { Location } from '@angular/common';
import { SpecializationService } from '../../specialization/services/specialization.service';
import { DoctorService } from '../../doctor/services/doctor.service';
import { Specialization } from '../../specialization/models/specialization.model';
import { Doctor } from '../../doctor/models/doctor.model';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router'; // Import RouterModule
import { DoctorDetailDto } from '../../doctor/models/doctor-detail.model';

@Component({
  selector: 'app-clinic-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule], // Thêm RouterModule vào imports
  templateUrl: './clinic-detail.component.html',
  styleUrls: ['./clinic-detail.component.css']
})
export class ClinicDetailComponent implements OnInit {
  clinic: Clinic | null = null;
  errorMessage: string | null = null;
  clinicId!: number;
  specializations: Specialization[] = [];
  doctors: DoctorDetailDto[] = [];
  selectedSpecializationId: number | null = null; // null đại diện cho "Tất cả"

  constructor(
    private route: ActivatedRoute,
    private clinicService: ClinicService,
    private location: Location,
    private specializationService: SpecializationService,
    private doctorService: DoctorService
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;
    if (id !== null && !isNaN(id)) {
      this.clinicId = id;
      this.loadClinicDetails(id);
      this.loadSpecializations();
      this.loadDoctors(); // Tải danh sách bác sĩ mặc định (Tất cả)
    } else {
      this.errorMessage = 'Invalid clinic ID.';
    }
  }

  loadClinicDetails(id: number): void {
    this.clinicService.getClinicById(id).subscribe({
      next: (data: Clinic) => {
        this.clinic = data;
      },
      error: (err) => {
        if (err.status === 404) {
          this.errorMessage = 'Clinic not found.';
        } else {
          this.errorMessage = 'Failed to load clinic details. Please try again later.';
        }
        console.error(err);
      }
    });
  }

  loadSpecializations(): void {
    this.specializationService.getSpecializationsByClinicId(this.clinicId).subscribe({
      next: (data) => {
        this.specializations = data;
      },
      error: (err) => {
        console.error('Error loading specializations:', err);
        this.errorMessage = 'Failed to load specializations.';
      }
    });
  }

  loadDoctors(): void {
    if (this.selectedSpecializationId === null) {
      // Nếu chọn "Tất cả", lấy toàn bộ bác sĩ của phòng khám
      this.clinicService.getDoctorsByClinicId(this.clinicId).subscribe({
        next: (data) => {
          this.doctors = data;
          console.log(this.doctors);
        },
        error: (err) => {
          console.error('Error loading doctors:', err);
          this.errorMessage = 'Failed to load doctors.';
        }
      });
    } else {
      // Nếu chọn một chuyên khoa cụ thể, lấy bác sĩ theo chuyên khoa và phòng khám
      this.doctorService
        .getDoctorsBySpecializationAndClinic(this.selectedSpecializationId, this.clinicId)
        .subscribe({
          next: (data) => {
            this.doctors = data;
            console.log(this.doctors);
          },
          error: (err) => {
            console.error('Error loading doctors:', err);
            this.errorMessage = 'Failed to load doctors.';
          }
        });
    }
  }

  onSpecializationChange(specializationId: number | null): void {
    this.selectedSpecializationId = specializationId;
    this.loadDoctors();
  }

  goBack(): void {
    this.location.back();
  }
}