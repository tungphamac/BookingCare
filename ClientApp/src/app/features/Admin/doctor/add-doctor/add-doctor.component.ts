import { Component } from '@angular/core';
import { AddDoctor } from '../model/doctor.model';
import { DoctorService } from '../services/list-doctor.service';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { Specialization } from '../../specialization/models/specialization.model';
import { ClinicService } from '../../clinic/services/services.service';
import { SpecializationService } from '../../specialization/services/services.service';
import { Clinic } from '../../clinic/models/clinic2.model';



@Component({
  selector: 'app-add-doctor',
  templateUrl: './add-doctor.component.html',
  styleUrls: ['./add-doctor.component.css'],
  imports:[CommonModule, FormsModule,RouterModule ]
})
export class AddDoctorComponent {
  newDoctor: AddDoctor = new AddDoctor(); // Sử dụng model AddDoctor
  specializations: Specialization[] = [];
  clinics: Clinic[] = [];
  isAddDoctorVisible: boolean = true; // Biến kiểm soát modal Add Doctor
  selectedAvatar: string | ArrayBuffer | null = null; // Biến lưu ảnh được chọn

  constructor(private doctorService: DoctorService,private router: Router,private specializationService: SpecializationService,
    private clinicService: ClinicService) {}
    ngOnInit() {
      this.loadSpecializations();
      this.loadClinics();
    }
    loadSpecializations() {
      this.specializationService.getAll().subscribe({
        next: (data) => {
          this.specializations = data;
        },
        error: (error) => {
          console.error('Error loading specializations', error);
        }
      });
    }
  
    loadClinics() {
      this.clinicService.getAllClinics().subscribe({
        next: (data) => {
          this.clinics = data;
        },
        error: (error) => {
          console.error('Error loading clinics', error);
        }
      });
    }
  
  // Hàm đóng modal Add Doctor
  closeAddDoctorModal() {
    // Điều hướng đến trang Doctor/getall
    this.router.navigate(['/Doctor/getall']);
  }

  // Hàm thêm bác sĩ mới
  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        this.selectedAvatar = reader.result; // Lưu ảnh vào biến
        this.newDoctor.avatar = this.selectedAvatar as string; // Cập nhật vào model
      };
      reader.readAsDataURL(file); // Chuyển file thành base64 để hiển thị preview
    }
  }

  // Hàm thêm bác sĩ mới
  addDoctor() {
    this.doctorService.addDoctor(this.newDoctor).subscribe(
      (response) => {
        alert('Doctor added successfully!');
        this.newDoctor = new AddDoctor(); // Reset form sau khi thêm thành công
        this.selectedAvatar = null; // Reset avatar sau khi thêm thành công
        this.closeAddDoctorModal(); // Đóng modal sau khi thêm thành công
      },
      (error) => {
        alert('Error adding doctor');
        console.error(error);
      }
    );
  }
}
