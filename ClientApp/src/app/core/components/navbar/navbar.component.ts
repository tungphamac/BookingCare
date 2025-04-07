import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { User } from '../../../features/auth/login/Models/user.model';
import { AuthService } from '../../../features/auth/services/auth.service';
import { NotificationListComponent } from '../../../features/notification/notification-list/notification-list.component';
import { NotificationService } from '../../../features/notification/services/notification.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [FormsModule, RouterLink, CommonModule, NotificationListComponent],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  user?: User;
  showNotifications: boolean = false;
  unreadCount: number = 0;

  constructor(
    private authService: AuthService,
    private router: Router,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.authService.user().subscribe({
      next: response => {
        this.user = response;
      }
    });
    this.user = this.authService.getUser();
    this.loadUnreadCount();
  }

  loadUnreadCount(): void {
    this.notificationService.getNotifications().subscribe({
      next: (notifications) => {
        this.unreadCount = notifications.filter(n => !n.isRead).length;
      },
      error: (err) => {
        console.error('Không thể tải số thông báo chưa đọc:', err);
      }
    });
  }

  onLogout() {
    this.authService.logout();
  }

  onNavigateToProfile(): void {
    if (this.user?.role === 'Patient') {
      this.router.navigate(['/patients', this.user.id]);
    } else if (this.user?.role === 'Doctor') {
      this.router.navigate(['/doctor-profile', this.user.id]);
    } else {
      alert('Vai trò không xác định!');
    }
  }

  onNavigateToSchedule(): void {
    if (this.user?.role === 'Doctor') {
      this.router.navigate(['/schedules'], { queryParams: { doctorId: this.user.id } });
    } else {
      alert('Chỉ bác sĩ mới có quyền truy cập quản lý lịch!');
    }
  }

  toggleNotifications(): void {
    this.showNotifications = !this.showNotifications;
    if (this.showNotifications) {
      this.loadUnreadCount();
    }
  }
}