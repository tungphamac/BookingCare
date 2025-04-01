import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { Notification } from '../models/notification.model';


@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private http: HttpClient) {}

  // Lấy danh sách thông báo của người dùng
  getNotifications(userId: number): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${API_URL}/Notification/notifications?userId=${userId}`);
  }


  // Phản hồi cuộc hẹn (chỉ dành cho bác sĩ)
  respondToAppointment(appointmentId: number, accept: boolean): Observable<any> {
    return this.http.post(`${API_URL}/Notification/respond/${appointmentId}?accept=${accept}`, {});
  }
}