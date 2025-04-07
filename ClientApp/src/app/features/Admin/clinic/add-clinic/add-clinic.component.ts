import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { Clinic } from '../models/clinic2.model';
import { ClinicService } from '../services/services.service';

@Component({
  selector: 'app-clinic-create',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-clinic.component.html',
  styleUrls: ['./add-clinic.component.css']
})
export class ClinicCreateComponent {
  clinic: Clinic = {
    name: '',
    address: '',
    phone: 0,
    introduction: '',
    createAt: new Date()
  };
  isDarkMode = false;

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
  }
  
  errorMessage = '';

  constructor(private clinicService: ClinicService, private router: Router) {}

  // addClinic(): void {
  //   this.clinicService.addClinic(this.clinic).subscribe({
  //     next: () => {
  //       alert('Thêm phòng khám thành công!');
  //       this.router.navigate(['/Clinic/getall']);
  //     },
  //     error: (err) => {
  //       console.error('❌ Error adding clinic:', err);
  //       this.errorMessage = 'Thêm thất bại. Vui lòng kiểm tra lại dữ liệu.';
  //     }
  //   });
  // }
  addClinic(form: any): void {
    if (form.invalid) {
      return; // ⛔ Không gửi nếu form chưa hợp lệ
    }
  
    this.clinicService.addClinic(this.clinic).subscribe({
      next: () => {
        alert('Thêm phòng khám thành công!');
        this.router.navigate(['/Clinic/get-all-clinics']);
      },
      error: (err) => {
        console.error('❌ Error adding clinic:', err);
        this.errorMessage = 'Thêm thất bại. Vui lòng kiểm tra lại dữ liệu.';
      }
    });
  }
  goBack() {
    this.router.navigate(['/Clinic/get-all-clinics']);
  }
  
}
