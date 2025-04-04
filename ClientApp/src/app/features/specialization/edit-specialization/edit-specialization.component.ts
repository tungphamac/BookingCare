import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Specialization } from '../models/specialization.model';
import { SpecializationService } from '../services/services.service';

@Component({
  selector: 'app-edit-specialization',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './edit-specialization.component.html',
  styleUrls: ['./edit-specialization.component.css']
})
export class EditSpecializationComponent implements OnInit {
  specialization: Specialization = {
    id: 0,
    name: '',
    description: '',
    image: ''
  };
  previewImageUrl: string | null = null;
  selectedFile: File | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private specializationService: SpecializationService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.specializationService.getById(id).subscribe({
      next: (data) => {
        this.specialization = data;
        this.previewImageUrl = `https://localhost:7182/uploads/${data.image}`;
      },
      error: () => {
        alert('Không tìm thấy chuyên khoa!');
        this.router.navigate(['/Specialization/getall']);
      }
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.selectedFile = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.previewImageUrl = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  updateSpecialization(): void {
    const updateData = { ...this.specialization };
  
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('file', this.selectedFile); // Gửi file ảnh lên server
  
      // Gửi ảnh lên server
      this.specializationService.uploadSpecialization(formData).subscribe({
        next: (res) => {
          updateData.image = res.fileName;  // Cập nhật tên file ảnh
          this.sendUpdate(updateData);  // Gửi dữ liệu chuyên khoa
        },
        error: (err) => {
          console.error('Lỗi khi upload ảnh:', err);
          alert('Upload ảnh thất bại!');
        }
      });
    } else {
      this.sendUpdate(updateData);
    }
  }
  
  
  sendUpdate(data: Specialization): void {
    if (!data.id) return;  // Kiểm tra ID hợp lệ
    this.specializationService.update(data.id, data).subscribe({
      next: () => {
        alert('Cập nhật thành công!');
        this.router.navigate(['/Specialization/getall']);
      },
      error: (err) => {
        console.error('Lỗi khi cập nhật chuyên khoa:', err);
        alert('Cập nhật thất bại!');
      }
    });
  }
  
  
  cancel(): void {
    this.router.navigate(['/Specialization/getall']);
  }
}
