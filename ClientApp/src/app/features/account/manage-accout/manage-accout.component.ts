import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PartientVm } from './Models/partient-viewmodel';

@Component({
  selector: 'app-manage-accout',
  imports: [CommonModule, RouterModule],
  templateUrl: './manage-accout.component.html',
  styleUrl: './manage-accout.component.css'
})
export class ManageAccoutComponent implements OnInit {
  patient: PartientVm | null = null;

  constructor(private route: ActivatedRoute, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    // Lấy thông tin người dùng hiện tại từ AuthService
    const currentUser = this.authService.getUser();
    if (currentUser) {
      // Nếu có user hiện tại, gọi API để lấy thông tin đầy đủ dựa trên email hoặc ID
      const userId = this.route.snapshot.paramMap.get('id') || currentUser.email; // Dùng email nếu không có ID
      this.fetchPatientData(userId);
    } else {
      // Nếu không có user hiện tại, lấy ID từ route params
      const userId = this.route.snapshot.paramMap.get('id');
      if (userId) {
        this.fetchPatientData(userId);
      }
    }
  }

  private fetchPatientData(id: string): void {
    this.authService.getUserById(id).subscribe(
      (data: PartientVm) => {
        this.patient = data; // Gán dữ liệu từ API vào patient
      },
      (error) => {
        console.error('Lỗi khi lấy thông tin bệnh nhân:', error);
        this.patient = null; // Xử lý lỗi nếu cần
      }
    );
  }

  goBack(): void {
    this.router.navigate(['/']); // Quay lại trang chính
  }
}

