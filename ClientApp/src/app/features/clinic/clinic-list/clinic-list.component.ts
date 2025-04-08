// src/app/features/clinic/clinic-list/clinic-list.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ClinicService } from '../services/clinic.service';
import { Clinic } from '../models/clinic.model';


@Component({
  selector: 'app-clinic-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './clinic-list.component.html',
  styleUrls: ['./clinic-list.component.css']
})
export class ClinicListComponent implements OnInit {
  clinics: Clinic[] = [];
  loading: boolean = true;
  error: string | null = null;

  constructor(private clinicService: ClinicService) { }

  ngOnInit(): void {
    this.fetchClinics();
  }

  fetchClinics(): void {
    this.loading = true;
    this.clinicService.getAllClinics().subscribe({
      next: (data) => {
        this.clinics = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load clinics. Please try again later.';
        this.loading = false;
        console.error('Error fetching clinics:', err);
      }
    });
  }
}