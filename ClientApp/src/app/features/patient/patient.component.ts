import { Component, OnInit } from '@angular/core';
import { Patient } from './models/patient.model';
import { PatientService } from './services/patient.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-patients',
  templateUrl: './patient.component.html',
 
  styleUrls: ['./patient.component.css'],
  imports: [
    CommonModule,
    RouterModule,        // ✅ Thêm dòng này để dùng [routerLink]
    HttpClientModule,
    // các component khác nếu có
  ],
})
export class PatientsComponent implements OnInit {
  patients: Patient[] = [];

  constructor(private patientService: PatientService) { }

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.patientService.getPatients().subscribe({
      next: (data) => {
        this.patients = data;
      },
      error: (error) => {
        console.error('There was an error!', error);
      }
    });
  }
  editPatient(userId?: number): void {
    if (userId === undefined) {
      console.error('User ID is undefined');
      return;
    }
    console.log('Editing patient with ID:', userId);
    // Remaining logic here
  }
  
  deletePatient(id: number): void {
    if(confirm("Are you sure to delete this patient?")) {
        this.patientService.deletePatient(id).subscribe({
            next: () => {
                this.patients = this.patients.filter(patient => patient.userId !== id);
                alert('Patient deleted successfully');
            },
            error: (err) => {
                console.error('Error deleting patient:', err);
                alert('Failed to delete patient: ' + err.error);
            }
        });
    }
}

  
}
