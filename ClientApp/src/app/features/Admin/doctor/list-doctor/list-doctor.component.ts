import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../services/list-doctor.service';
import { Doctor } from '../model/doctor.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

import { Specialization } from '../../specialization/models/specialization.model';
import { Clinic } from '../../clinic/models/clinic2.model';
import { SpecializationService } from '../../specialization/services/services.service';
import { ClinicService } from '../../clinic/services/services.service';


@Component({
  selector: 'app-doctor-list',
  templateUrl: './list-doctor.component.html',
  styleUrls: ['./list-doctor.component.css'],
  imports: [CommonModule, FormsModule, RouterModule, ],
})
export class DoctorListComponent implements OnInit {
  doctors: Doctor[] = [];
  
  searchText: string = '';
  specializations: Specialization[] = [];

  clinics: Clinic[] = [];
  selectedDoctor: any = null; // Store the selected doctor
  selectedAvatar: string | null = null;

  constructor(private doctorService: DoctorService,private specializationService: SpecializationService,
    private clinicService: ClinicService,private router: Router) { }

  ngOnInit(): void {
    this.loadData(); // Gọi loadData khi component được khởi tạo
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

  // Fetch doctors from the backend
  loadData(): void {
    this.doctorService.getDoctors().subscribe(
      (data) => {
        this.doctors = data; // Cập nhật danh sách bác sĩ
        

      },
      (error) => {
        console.error('Error fetching doctor list', error); // Xử lý lỗi nếu có
      }
    );
  }
 

  // View doctor details
  viewDoctor(doctor: any): void {
    this.selectedDoctor = doctor; // Set the selected doctor to view details
  }

  closeDetails(): void {
    this.selectedDoctor = null; // Close the details view
  }

  // Phương thức để thêm bác sĩ mới
  addDoctor(): void {
    console.log("Add Doctor clicked");
    // Thực hiện hành động thêm bác sĩ, có thể mở modal hay chuyển hướng đến trang khác
  }

  // Phương thức tìm kiếm bác sĩ


  // Delete doctor
  deleteDoctor(id: number): void {
    if (confirm('Are you sure you want to delete this doctor?')) {
      this.doctorService.deleteDoctor(id).subscribe(() => {
        this.loadData();  // Refresh list after deletion
      });
    }
  }

  // Update doctor
  updateDoctor(doctor: any) {
    this.doctorService.updateDoctor(doctor.id, doctor).subscribe({
      next: (response) => {
        alert('Doctor updated successfully!');
        this.closeDetails();
        // this.router.navigate(['/Doctor/getall']); // Chuyển hướng khi cập nhật thành công
        this.loadData();
      },
      error: (error) => {
        console.error('Error updating doctor:', error);
        alert(`Error: ${error.error.title} - ${JSON.stringify(error.error.errors)}`);
      }
    });
  }
  
  
  onFileSelected(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = () => {
            // Giả sử 'avatar' là thuộc tính lưu ảnh của bác sĩ
            this.selectedDoctor.avatar = reader.result as string;
            this.selectedAvatar = this.selectedDoctor.avatar; // Cập nhật cho selectedAvatar để hiển thị
        };
        reader.readAsDataURL(file);
    }

}

  onDoctorSelect(doctor: any): void {
    this.selectedAvatar = doctor.avatar;  // Assuming 'doctor' has a property 'avatar'
  }
  get filteredDoctors(): Doctor[] {
    if (!this.searchText) return this.doctors;  // Nếu không có gì trong thanh tìm kiếm, trả về tất cả các bác sĩ
    return this.doctors.filter(doctor => 
        doctor.userName.toLowerCase().includes(this.searchText.toLowerCase())
    );
}

 
}


