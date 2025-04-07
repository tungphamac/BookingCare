import { Component } from '@angular/core';
import { DoctorService } from '../services/doctor.service';
import { ClinicService } from '../../clinic/services/clinic.service';
import { SpecializationService } from '../../specialization/services/specialization.service';
import { ActivatedRoute } from '@angular/router';

import { Clinic } from '../../clinic/models/clinic';
import { Specialization } from '../../specialization/models/specialization.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-doctor-update',
  imports: [CommonModule, FormsModule],
  templateUrl: './doctor-update.component.html',
  styleUrl: './doctor-update.component.css'
})
export class DoctorUpdateComponent {
  doctor: any = {
    name: '',
    gender: true,
    email: '',
    phone: '',
    address: '',
    avatar: '',  // URL to preview the avatar
    achievement: '',
    description: '',
    specializationId: 0,
    clinicId: 0
  };

  avatarFile: File | null = null;
  specializations: Specialization[] = [];
  clinics: Clinic[] = [];
  doctorId: number = 0;

  constructor(
    private doctorService: DoctorService,
    private clinicService: ClinicService,
    private specializationService: SpecializationService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.doctorId = +params['doctorId'];

      if (this.doctorId) {
        Promise.all([this.getSpecializations(), this.getClinics()]).then(() => {
          this.getDoctorDetails(this.doctorId);
        });
      }
    });
  }

  getSpecializations(): Promise<void> {
    return new Promise((resolve) => {
      this.specializationService.getAllSpecializations().subscribe({
        next: (response) => {
          this.specializations = response;
          resolve();
        },
        error: (error) => {
          console.error('Error fetching specializations:', error);
          resolve(); // Dù có lỗi cũng gọi resolve để không làm delay các thao tác khác
        }
      });
    });
  }

  getClinics(): Promise<void> {
    return new Promise((resolve) => {
      this.clinicService.getAllClinics().subscribe({
        next: (response) => {
          this.clinics = response;
          resolve();
        },
        error: (error) => {
          console.error('Error fetching clinics:', error);
          resolve();
        }
      });
    });
  }
  // Fetch the doctor's details based on the doctorId
  getDoctorDetails(doctorId: number): void {
    this.doctorService.getDoctorById(doctorId).subscribe({
      next: (response) => {
        this.doctor = response;

        // Nếu avatar có dữ liệu, cập nhật lại đường dẫn
        if (this.doctor.avatar) {
          console.log('Avatar URL:', this.doctor.avatar); // Kiểm tra đường dẫn

          this.doctor.avatar = `http://localhost:5032/uploads/${this.doctor.avatar}`;
          console.log('sau khi them url: ', this.doctor.avatar);
        }
      },
      error: (error) => {
        console.error('Error fetching doctor details:', error);
      }
    });
  }

  // Trigger file input for avatar
  triggerAvatarInput(): void {
    const input = document.getElementById('avatarInput') as HTMLInputElement;
    input.click();
  }

  onAvatarChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files ? input.files[0] : null;  // Check if file selected

    if (file) {
      this.avatarFile = file; // Assign file to avatarFile
      this.previewAvatar(file); // Preview the selected avatar file
    } else {
      console.log("No file selected");
    }
  }

  previewAvatar(file: File): void {
    if (!file) {
      console.warn("No file selected");
      return;
    }

    // Kiểm tra định dạng file (chỉ cho phép ảnh)
    if (!file.type.startsWith("image/")) {
      alert("Please select a valid image file (JPG, PNG, etc.)");
      return;
    }

    // Kiểm tra kích thước file (giới hạn 5MB)
    const maxSize = 5 * 1024 * 1024; // 5MB
    if (file.size > maxSize) {
      alert("Image size must be less than 5MB");
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      this.doctor.avatar = reader.result as string; // Gán URL xem trước
    };
    console.log(this.doctor.avatar)
    reader.onerror = (error) => {
      console.error("Error reading file:", error);
      alert("Error previewing image");
    };

    reader.readAsDataURL(file);
  }

  // Submit form data
  onSubmit(): void {
    if (!this.avatarFile) {
      alert('Please select an avatar image!');
      return; // Stop if no avatar selected
    }

    const formData = new FormData();
    formData.append('name', this.doctor.name);
    formData.append('gender', this.doctor.gender.toString());
    formData.append('email', this.doctor.email);
    formData.append('phone', this.doctor.phone || '');
    formData.append('address', this.doctor.address);
    formData.append('achievement', this.doctor.achievement);
    formData.append('description', this.doctor.description);

    if (this.avatarFile) {
      formData.append('avatar', this.avatarFile, this.avatarFile.name); // Append the avatar file
      console.log(this.avatarFile);
    }

    // Send form data to the backend
    this.doctorService.updateDoctorProfile(this.doctorId, formData).subscribe({
      next: (response) => {
        console.log('Profile updated successfully:', response);
        alert('Profile saved successfully!');
      },
      error: (error) => {
        console.error('Error updating profile:', error, this.doctor);
        alert('Error saving profile.');
      }
    });
  }
}
