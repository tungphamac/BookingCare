import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { AppointmentCreate } from '../models/appointment-create.model';
import { AppointmentDetail } from '../models/appointment-detail.model';
import { AppointmentStatus } from '../models/appointment-status.enum';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private apiUrl = `${API_URL}/Appointment`;

  constructor(private http: HttpClient) {}

  // Lấy header với token
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('Authentication');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  // Tạo lịch hẹn (Patient)
  createAppointment(appointment: AppointmentCreate): Observable<any> {
    return this.http.post<any>(this.apiUrl, appointment, { headers: this.getHeaders() });
  }

  // Quản lý danh sách lịch hẹn (Doctor: Accept/Decline - Patient: Cancel)
  manageAppointment(id: number, status: AppointmentStatus, pageNumber: number = 1, pageSize: number = 10): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}/manage?status=${status}&pageNumber=${pageNumber}&pageSize=${pageSize}`, null, { headers: this.getHeaders() });
  }

  // Cập nhật lịch hẹn (Patient)
  updateAppointment(id: number, appointment: AppointmentCreate): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, appointment, { headers: this.getHeaders() });
  }

  // Xem chi tiết lịch hẹn (Admin, Doctor, Patient)
  getAppointmentDetail(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }

  // Lấy danh sách lịch hẹn (Doctor hoặc Patient)
  getAppointments(pageNumber: number = 1, pageSize: number = 10): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers: this.getHeaders() });
  }
}
