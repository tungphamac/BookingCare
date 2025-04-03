import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SpecializationService } from '../services/services.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-add-specialization',
  templateUrl: './add-specialization.component.html',
  styleUrls: ['./add-specialization.component.css'],
  standalone: true,
  imports: [
    FormsModule,       // ✅ Cho [(ngModel)]
    CommonModule,      // ✅ Cho *ngIf, *ngFor
    RouterModule       // ✅ Cho routerLink
  ]
})
export class AddSpecializationComponent {
  specialization = {
    name: '',
    description: ''
  };
  selectedFile?: File;
  previewImageUrl: string | ArrayBuffer | null = null;

  constructor(private specializationService: SpecializationService, private router: Router) {}

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.previewImageUrl = reader.result;
      };
      reader.readAsDataURL(file);
    }
  }

  addSpecialization(): void {
    if (!this.specialization.name || !this.specialization.description || !this.selectedFile) {
      alert('Vui lòng nhập đủ thông tin và chọn ảnh!');
      return;
    }

    const formData = new FormData();
    formData.append('name', this.specialization.name);
    formData.append('description', this.specialization.description);
    formData.append('image', this.selectedFile);

    this.specializationService.uploadSpecialization(formData).subscribe({
      next: () => {
        alert('Thêm chuyên khoa thành công!');
        this.router.navigate(['/Specialization/getall']);
      },
      error: (err) => {
        console.error('Lỗi khi upload:', err);
        alert('Thêm chuyên khoa thất bại!');
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/Specialization/getall']);
  }
  
  
  
}
