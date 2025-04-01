import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NotificationService } from '../services/notification.service';
import { Notification } from '../models/notification.model';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-notification-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent implements OnInit {
  notifications: Notification[] = [];
  errorMessage: string | null = null;
  isLoading: boolean = false;
  userId: number | null = null;

  constructor(
    private notificationService: NotificationService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // Lấy userId từ AuthService
    const user = this.authService.getUser();
    if (user && user.id) {
      this.userId = user.id;
      this.loadNotifications();
    } else {
      this.errorMessage = 'Vui lòng đăng nhập để xem thông báo.';
    }
  }

  loadNotifications(): void {
    if (!this.userId) return;

    this.isLoading = true;
    this.notificationService.getNotifications(this.userId).subscribe({
      next: (notifications) => {
        this.notifications = notifications;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải thông báo. Vui lòng thử lại sau.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }
}