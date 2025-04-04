import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientService } from '../services/patient.service';
import { Patient } from '../models/patient';
import { Location } from '@angular/common';
import { PatientDetail } from '../models/patient-detail.model';

@Component({
  selector: 'app-patient-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './patientc-detail.component.html',
  styleUrl: './patientc-detail.component.css'
})
export class PatientDetailComponent implements OnInit {
  patient: PatientDetail | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private patientService: PatientService,
    private location: Location
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;
    if (id !== null && !isNaN(id)) {
      this.patientService.getPatientDetail(id).subscribe({
        next: (data: PatientDetail) => {
          this.patient = data;
        },
        error: (err) => {
          if (err.status === 404) {
            this.errorMessage = 'Patient not found.';
          } else if (err.status === 401) {
            this.errorMessage = 'You are not authorized to view this patient\'s details.';
          } else {
            this.errorMessage = 'Failed to load patient details. Please try again later.';
          }
          console.error(err);
        }
      });
    } else {
      this.errorMessage = 'Invalid patient ID.';
    }
  }

  goBack(): void {
    this.location.back();
  }
}