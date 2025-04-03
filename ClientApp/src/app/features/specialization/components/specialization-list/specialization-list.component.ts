import { Component, OnInit } from '@angular/core';
import { SpecializationService } from '../../services/specialization.service';
import { SpecializationDetailDto } from '../../models/specialization-detail.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-specialization-list',
  templateUrl: './specialization-list.component.html',
  styleUrls: ['./specialization-list.component.css'],
  imports: [CommonModule, FormsModule]
})
export class SpecializationListComponent implements OnInit {
  specializations: SpecializationDetailDto[] = [];
  errorMessage: string | null = null;
  isLoading: boolean = false;

  constructor(private specializationService: SpecializationService) {}

  ngOnInit(): void {
    this.loadSpecializations();
  }

  loadSpecializations(): void {
    this.isLoading = true;
    this.specializationService.getAllSpecializations().subscribe({
      next: (data) => {
        this.specializations = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải danh sách chuyên khoa. Vui lòng thử lại sau.';
        console.error(err);
        this.isLoading = false;
      }
    });
  }
}
