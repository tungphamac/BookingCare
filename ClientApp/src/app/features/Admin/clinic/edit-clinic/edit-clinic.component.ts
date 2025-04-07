import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Clinic } from '../models/clinic2.model';
import { ClinicService } from '../services/services.service';

@Component({
  selector: 'app-clinic-edit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-clinic.component.html',
  styleUrls: ['./edit-clinic.component.css']
})
export class ClinicEditComponent implements OnInit {
  clinic: Clinic = {
    id: 0,
    name: '',
    address: '',
    phone: 0,
    introduction: '',
    createAt: new Date()
  };

  errorMessage = '';
  isLoading = true;

  constructor(
    private route: ActivatedRoute,
    private clinicService: ClinicService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.clinicService.getClinicById(id).subscribe({
      next: (data) => {
        this.clinic = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = 'Không tải được thông tin phòng khám.';
        this.isLoading = false;
      }
    });
  }

  updateClinic(): void {
    if (!this.clinic.id) return;
    this.clinicService.updateClinic(this.clinic.id, this.clinic).subscribe({
      next: () => {
        alert('Cập nhật thành công!');
        this.router.navigate(['/Clinic/get-all-clinics']);
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = 'Cập nhật thất bại.';
      }
    });
  }
  goBack(): void {
    this.router.navigate(['/Clinic/get-all-clinics']);
  }
  
}
