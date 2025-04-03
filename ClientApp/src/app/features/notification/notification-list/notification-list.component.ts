import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../services/notification.service';
import { NotificationDto } from '../models/notification.model';
import { AppointmentDetailDto } from '../models/appointment-detail.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, ActivatedRoute } from '@angular/router'; // Thêm ActivatedRoute

@Component({
  selector: 'app-notification-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent implements OnInit {
  notifications: NotificationDto[] = [];
  selectedAppointment: AppointmentDetailDto | null = null;
  userId: number | null = null; // Không hardcode userId nữa
  errorMessage: string | null = null;
  isLoading: boolean = false;

  constructor(
    private notificationService: NotificationService,
    private route: ActivatedRoute // Inject ActivatedRoute để lấy query parameter
  ) {}

  ngOnInit(): void {
    // Lấy userId từ query parameter
    this.route.queryParams.subscribe(params => {
      const userIdParam = params['userId'];
      if (userIdParam) {
        this.userId = +userIdParam; // Chuyển string thành number
        this.loadNotifications();
      } else {
        this.errorMessage = 'Vui lòng cung cấp userId trong URL (ví dụ: /notifications?userId=10).';
      }
    });
  }

  loadNotifications(): void {
    if (!this.userId) {
      this.errorMessage = 'Không tìm thấy ID người dùng.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    this.notifications = [];

    this.notificationService.getNotifications(this.userId).subscribe({
      next: (notifications) => {
        this.notifications = notifications;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.Message || 'Không thể tải danh sách thông báo. Vui lòng thử lại sau.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  viewAppointmentDetail(appointmentId: number): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.selectedAppointment = null;

    this.notificationService.getAppointmentDetail(appointmentId).subscribe({
      next: (appointment) => {
        this.selectedAppointment = appointment;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.Message || 'Không thể tải chi tiết cuộc hẹn. Vui lòng thử lại sau.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }
}