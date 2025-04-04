// src/app/features/patient/patient-edit/patient-edit.component.ts

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Patient } from '../models/patient.model';
import { PatientService } from '../services/patient.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-patient-edit',
  standalone: true,
 
  templateUrl: './edit-patient.component.html',
  styleUrls: ['./edit-patient.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class PatientEditComponent implements OnInit {
  [x: string]: any;
  patient: Patient | null = null;
  isLoading = false;
  errorMessage = '';

  constructor(private patientService: PatientService,private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    console.log('Editing patient with ID:', id); // ✅ In ra để check
    this.loadPatient(id);
  }
  

  loadPatient(id: number): void {
    this.isLoading = true;
    this.patientService.getPatientById(id).subscribe({
      next: (data) => {
        this.patient = data;
        console.log('Loaded patient:', this.patient); // ← check ở đây có userId không
        this.isLoading = false;
        
      }
      ,
      error: (error) => {
        this.errorMessage = 'Could not load patient data';
        this.isLoading = false;
        console.error('There was an error!', error);
      }
    });
  }

  updatePatient(): void {
    if (this.patient && this.patient.userId != null) {
      this.patientService.updatePatient(this.patient.userId, this.patient).subscribe({
        next: () => {
          alert('Patient updated successfully');
        },
        error: (error) => {
          this.errorMessage = 'Failed to update patient';
          console.error('There was an error!', error);
        }
      });
    } else {
      console.warn('Patient or userId is undefined!');
    }
  }
  
  
  
}
