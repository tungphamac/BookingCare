import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';

import { NotificationDto } from '../models/notification.model';
import { AppointmentDetailDto } from '../models/appointment-detail.model';


@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private http: HttpClient) {}


  // Lấy danh sách thông báo theo userId
  getNotifications(userId: number): Observable<NotificationDto[]> {
    return this.http.get<NotificationDto[]>(`${API_URL}/Notification/notifications?userId=${userId}`);
  }

  // Lấy chi tiết cuộc hẹn theo appointmentId
  getAppointmentDetail(appointmentId: number): Observable<AppointmentDetailDto> {
    return this.http.get<AppointmentDetailDto>(`${API_URL}/Notification/appointment/${appointmentId}`);

  }
}
