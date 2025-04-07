// src/app/features/notification/services/notification.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { NotificationDto } from '../models/notification.model';
import { AuthService } from '../../auth/services/auth.service';
import { AppointmentDetail } from '../../appointment/models/appointment-detail.model';
import { AppointmentDetailDto } from '../models/appointment-detail.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  // Lấy danh sách thông báo theo userId
  getNotifications(): Observable<NotificationDto[]> {  // Sửa ở đây
    const userId = this.authService.getUser()?.id;
    if (!userId) {
      throw new Error('User ID not found. Please log in.');
    }
    return this.http.get<NotificationDto[]>(  // Sửa ở đây
      `${API_URL}/Notification/notifications?userId=${userId}`,
      { headers: this.getHeaders() }
    );
  }

  // Các phương thức khác giữ nguyên tạm thời
  getAppointmentDetail(appointmentId: number): Observable<{ data: AppointmentDetailDto }> {
    return this.http.get<{ data: AppointmentDetailDto }>(
      `${API_URL}/Notification/appointment/${appointmentId}`,
      { headers: this.getHeaders() }
    );
  }
}