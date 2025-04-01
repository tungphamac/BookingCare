import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router'; // Thêm Router
import { PatientSearchService } from '../services/patient-search.service'; // Sửa thành PatientSearchService
import { SearchResult } from '../models/search-result.model';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-patient-search',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './patient-search.component.html',
  styleUrls: ['./patient-search.component.css']
})
export class PatientSearchComponent implements OnInit {
  keyword: string = '';
  searchResult: SearchResult | null = null;
  errorMessage: string | null = null;
  isLoading: boolean = false;
  recentSearches: string[] = [];
  doctorId: number | null = null;

  constructor(
    private searchService: PatientSearchService, // Sửa thành PatientSearchService
    private authService: AuthService,
    private router: Router // Thêm Router
  ) {}

  ngOnInit(): void {
    // Lấy thông tin bác sĩ đang đăng nhập
    //this.doctorId = this.authService.getCurrentUserId(); // Bỏ comment để lấy doctorId

    // Khôi phục các tìm kiếm gần đây từ localStorage nếu có
    const savedSearches = localStorage.getItem('doctorPatientSearches');
    if (savedSearches) {
      this.recentSearches = JSON.parse(savedSearches);
    }

    // Kiểm tra xem người dùng hiện tại có phải là bác sĩ không
    // if (!this.authService.hasRole('Doctor')) {
    //   this.errorMessage = 'Bạn không có quyền truy cập chức năng này.';
    //   this.router.navigate(['/']); // Chuyển hướng về trang chủ nếu không phải bác sĩ
    // }
  }

  onSearch(): void {
    if (!this.doctorId) {
      this.errorMessage = 'Không thể xác định ID bác sĩ.';
      return;
    }

    if (!this.keyword.trim()) {
      this.errorMessage = 'Vui lòng nhập từ khóa tìm kiếm.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.searchResult = null;

    // Lưu từ khóa tìm kiếm vào danh sách tìm kiếm gần đây
    this.saveSearch(this.keyword);

    this.searchService.searchPatientsForDoctor(this.doctorId, this.keyword).subscribe({
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
    localStorage.setItem('doctorPatientSearches', JSON.stringify(this.recentSearches));
  }

  selectRecentSearch(keyword: string): void {
    this.keyword = keyword;
    this.onSearch();
  }

  viewMedicalRecord(medicalRecordId: number): void {
    // Chuyển hướng đến trang hồ sơ bệnh án (giả sử bạn có route /medical-records/:id)
    this.router.navigate(['/medical-records', medicalRecordId]);
  }
}