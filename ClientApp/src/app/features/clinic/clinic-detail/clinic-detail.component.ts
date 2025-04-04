import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ClinicService } from '../services/clinic.service';
import { Clinic } from '../models/clinic';
import { Location } from '@angular/common'; // Import Location

@Component({
  selector: 'app-clinic-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './clinic-detail.component.html',
  styleUrl: './clinic-detail.component.css'
})
export class ClinicDetailComponent implements OnInit {
  clinic: Clinic | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private clinicService: ClinicService,
    private location: Location // Inject Location
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;
    if (id !== null && !isNaN(id)) {
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
    } else {
      this.errorMessage = 'Invalid clinic ID.';
    }
  }

  goBack(): void {
    this.location.back(); // Quay lại trang trước
  }
}