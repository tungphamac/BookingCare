import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';


import { RouterModule } from '@angular/router';
import { ClinicService } from './services/clinic.service';
import { Clinic } from './models/clinic.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-clinic-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './clinic.component.html',
  styleUrls: ['./clinic.component.css']
})
export class ClinicListComponent implements OnInit {
  clinics: Clinic[] = [];
  isLoading = true;
  errorMessage = '';
 
  filteredClinics: Clinic[] = [];
  searchText: string = '';
  
  

  constructor(private clinicService: ClinicService) {}

  ngOnInit(): void {
    this.clinicService.getAllClinics().subscribe({
      next: (data) => {
        this.clinics = data;
        this.filteredClinics = data; // ✅ Gán luôn cho danh sách hiển thị
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load clinics.';
        this.isLoading = false;
        console.error('API error:', err);
      }
    });
  }
  
  deleteClinic(id?: number): void {
    if (id === undefined) return;
  
    if (confirm('Bạn có chắc chắn muốn xoá không?')) {
      this.clinicService.deleteClinic(id).subscribe({
        next: () => {
          alert('Đã xoá!');
          this.clinics = this.clinics.filter(c => c.id !== id);
        },
        error: (err) => {
          alert('Xoá thất bại!');
          console.error(err);
        }
      });
    }
  }
  filterClinics(): void {
    const keyword = this.searchText.toLowerCase();
    this.filteredClinics = this.clinics.filter(c =>
      c.name?.toLowerCase().includes(keyword)
    );
  }
  
}
