import { Component, OnInit } from '@angular/core';
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
export class SearchComponent implements OnInit {
  filter: string = 'Doctor'; // Mặc định là tìm kiếm bác sĩ
  keyword: string = '';
  searchResult: SearchResult | null = null;
  errorMessage: string | null = null;
  isLoading: boolean = false;
  recentSearches: string[] = [];

  constructor(private searchService: SearchService) {}

  ngOnInit(): void {
    // Khôi phục các tìm kiếm gần đây từ localStorage nếu có
    const savedSearches = localStorage.getItem('recentSearches');
    if (savedSearches) {
      this.recentSearches = JSON.parse(savedSearches);
    }
  }

  onSearch(): void {
    if (!this.keyword.trim()) {
      this.errorMessage = 'Vui lòng nhập từ khóa tìm kiếm.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.searchResult = null;

    // Lưu từ khóa tìm kiếm vào danh sách tìm kiếm gần đây
    this.saveSearch(this.keyword);

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

    // Lưu từ khóa tìm kiếm vào danh sách tìm kiếm gần đây
    this.saveSearch(this.keyword);

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

  // Lưu từ khóa tìm kiếm vào localStorage
  private saveSearch(keyword: string): void {
    // Nếu từ khóa đã tồn tại, xóa nó đi để đưa lên đầu danh sách
    this.recentSearches = this.recentSearches.filter(k => k !== keyword);
    
    // Thêm từ khóa mới vào đầu danh sách
    this.recentSearches.unshift(keyword);
    
    // Giới hạn số lượng từ khóa lưu trữ (ví dụ: 5)
    if (this.recentSearches.length > 5) {
      this.recentSearches = this.recentSearches.slice(0, 5);
    }
    
    // Lưu vào localStorage
    localStorage.setItem('recentSearches', JSON.stringify(this.recentSearches));
  }
}