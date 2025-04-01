import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SearchService } from '../services/search.service';
import { SearchResult } from '../models/search-result.model';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  filter: string = 'Doctor'; // Mặc định là tìm kiếm bác sĩ
  keyword: string = '';
  searchResult: SearchResult | null = null;
  errorMessage: string | null = null;
  isLoading: boolean = false;

  constructor(private searchService: SearchService) {}

  onSearch(): void {
    if (!this.keyword.trim()) {
      this.errorMessage = 'Vui lòng nhập từ khóa tìm kiếm.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.searchResult = null;

    this.searchService.generalSearch(this.filter, this.keyword).subscribe({
      next: (result) => {
        this.searchResult = result;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.Message || 'Không thể thực hiện tìm kiếm. Vui lòng thử lại sau.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  onSearchBySpecialization(): void {
    if (!this.keyword.trim()) {
      this.errorMessage = 'Vui lòng nhập từ khóa tìm kiếm.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.searchResult = null;

    this.searchService.searchBySpecialization(this.keyword).subscribe({
      next: (result) => {
        this.searchResult = result;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.Message || 'Không thể thực hiện tìm kiếm. Vui lòng thử lại sau.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }
}