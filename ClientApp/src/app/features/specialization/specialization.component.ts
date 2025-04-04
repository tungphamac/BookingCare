import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SpecializationService } from './services/services.service';
import { Specialization } from './models/specialization.model';
import { ImportComponent } from "../features/import/import.component";


@Component({
  selector: 'app-specialization',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ImportComponent],
  templateUrl: './specialization.component.html',
  styleUrls: ['./specialization.component.css']
})
export class SpecializationComponent implements OnInit {
  specializations: Specialization[] = [];
  filteredSpecializations: Specialization[] = [];
  searchText = '';
  isLoading = true;
  errorMessage = '';
  

  constructor(private specializationService: SpecializationService) {}

  ngOnInit(): void {
    this.specializationService.getAll().subscribe({
      next: (data) => {
        this.specializations = data;
        this.filteredSpecializations = data;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Không thể tải danh sách chuyên khoa.';
        this.isLoading = false;
      }
    });
  }

  filterSpecializations(): void {
    const keyword = this.searchText.toLowerCase();
    this.filteredSpecializations = this.specializations.filter(s =>
      s.name.toLowerCase().includes(keyword)
    );
    this.currentPage = 1; // reset về trang đầu
  }

  deleteSpecialization(id?: number): void {
    if (!id) return;
    if (confirm('Bạn có chắc chắn muốn xoá chuyên khoa này không?')) {
      this.specializationService.delete(id).subscribe(() => {
        this.specializations = this.specializations.filter(s => s.id !== id);
        this.filteredSpecializations = this.filteredSpecializations.filter(s => s.id !== id);
      });
    }
  }
  handleImgError(event: Event): void {
    const imgElement = event.target as HTMLImageElement;
  
    // Nếu ảnh hiện tại đã là ảnh fallback thì không thay nữa
    if (!imgElement.src.includes('default.jpg')) {
      imgElement.src = 'assets/default.jpg';
    }
  }
  
  // Biến phân trang
currentPage: number = 1;
itemsPerPage: number = 2;

get pagedSpecializations(): Specialization[] {
  const start = (this.currentPage - 1) * this.itemsPerPage;
  const end = start + this.itemsPerPage;
  return this.filteredSpecializations.slice(start, end);
}

get totalPages(): number {
  return Math.ceil(this.filteredSpecializations.length / this.itemsPerPage);
}

changePage(page: number): void {
  if (page >= 1 && page <= this.totalPages) {
    this.currentPage = page;
  }
}

  

}
