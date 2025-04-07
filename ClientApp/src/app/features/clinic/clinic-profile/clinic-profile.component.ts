import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ClinicService } from '../services/clinic.service';
import { Clinic } from '../models/clinic.model';
import { Location } from '@angular/common';

@Component({
  selector: 'app-clinic-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './clinic-profile.component.html',
  styleUrl: './clinic-profile.component.css'
})
export class ClinicProfileComponent implements OnInit {
  clinic: Clinic | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private clinicService: ClinicService,
    private location: Location
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
            this.errorMessage = 'Không tìm thấy thông tin phòng khám.';
          } else {
            this.errorMessage = 'Không thể tải thông tin phòng khám. Vui lòng thử lại sau.';
          }
          console.error(err);
        }
      });
    } else {
      this.errorMessage = 'ID phòng khám không hợp lệ.';
    }
  }

  goBack(): void {
    this.location.back();
  }
}
