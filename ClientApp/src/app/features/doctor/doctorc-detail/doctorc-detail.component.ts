import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DoctorService } from '../services/doctor.service';
import { Doctor } from '../models/doctor.model';
import { Location } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-doctor-detail',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './doctorc-detail.component.html',
  styleUrl: './doctorc-detail.component.css'
})
export class DoctorDetailComponent implements OnInit {
  doctor: Doctor | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private doctorService: DoctorService,
    private location: Location
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;
    if (id !== null && !isNaN(id)) {
      this.doctorService.getDoctorById(id).subscribe({
        next: (data: Doctor) => {
          this.doctor = data;
        },
        error: (err) => {
          if (err.status === 404) {
            this.errorMessage = 'Doctor not found.';
          } else {
            this.errorMessage = 'Failed to load doctor details. Please try again later.';
          }
          console.error(err);
        }
      });
    } else {
      this.errorMessage = 'Invalid doctor ID.';
    }
  }

  goBack(): void {
    this.location.back();
  }
}