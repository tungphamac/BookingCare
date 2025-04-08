// src/app/features/notification/notification-list/notification-list.component.ts
import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../services/notification.service';
import { NotificationDto } from '../models/notification.model';
import { AppointmentDetailDto } from '../models/appointment-detail.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterModule } from '@angular/router';
import { AppointmentDetail } from '../../appointment/models/appointment-detail.model';

@Component({
  selector: 'app-notification-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, RouterLink],
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css']
})
export class NotificationListComponent implements OnInit {
  notifications: NotificationDto[] = [];
  selectedAppointment: AppointmentDetailDto | null = null;
  errorMessage: string | null = null;
  isLoading: boolean = false;

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.loadNotifications();
  }

  loadNotifications(): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.notifications = [];

    this.notificationService.getNotifications().subscribe({
      next: (notifications) => {  // Sửa ở đây: không dùng response.data
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
      next: (response) => {
        this.selectedAppointment = response.data;  // Để nguyên vì chưa kiểm tra API này
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