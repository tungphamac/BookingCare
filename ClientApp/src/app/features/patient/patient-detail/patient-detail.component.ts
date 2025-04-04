import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PatientService } from '../services/patient.service';
import { PatientDetail } from '../models/patient-detail.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-patient-detail',
  imports: [CommonModule, FormsModule], 
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.css']
  
})
export class PatientDetailComponent implements OnInit {
  patient: PatientDetail | null = null;
  errorMessage: string | null = null;
  isLoading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private patientService: PatientService
  ) {}

  ngOnInit(): void {
    // Lấy ID từ route params
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadPatientDetail(+id);
    } else {
      this.errorMessage = 'Không tìm thấy ID bệnh nhân.';
    }
  }

  loadPatientDetail(id: number): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.patient = null;

    this.patientService.getPatientDetail(id).subscribe({
      next: (patient) => {
        this.patient = patient;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.Message || 'Không thể tải thông tin bệnh nhân. Vui lòng thử lại sau.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }
}