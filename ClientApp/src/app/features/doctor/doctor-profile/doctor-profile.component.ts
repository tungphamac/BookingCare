import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DoctorService } from '../services/doctor.service';
import { Doctor } from '../models/doctor.model';
import { Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './doctor-profile.component.html',
  styleUrl: './doctor-profile.component.css'
})
export class DoctorProfileComponent implements OnInit {
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
            this.errorMessage = 'Không tìm thấy thông tin bác sĩ.';
          } else {
            this.errorMessage = 'Không thể tải thông tin bác sĩ. Vui lòng thử lại sau.';
          }
          console.error(err);
        }
      });
    } else {
      this.errorMessage = 'ID bác sĩ không hợp lệ.';
    }
  }

  goBack(): void {
    this.location.back();
  }
}