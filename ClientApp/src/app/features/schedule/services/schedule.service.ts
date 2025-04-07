import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { Schedule } from '../models/schedule.model';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  // Hàm lấy header với token xác thực
  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  // Xem chi tiết schedule theo ID
  getScheduleById(id: number): Observable<Schedule> {
    const headers = this.getHeaders();
    return this.http.get<Schedule>(`${API_URL}/Schedule/get-schedule-by-id/${id}`, { headers });
  }

  // Xem danh sách tất cả schedules
  getAllSchedules(): Observable<Schedule[]> {
    const headers = this.getHeaders();
    return this.http.get<Schedule[]>(`${API_URL}/Schedule`, { headers });
  }

  // Xem danh sách schedules theo doctorId
  getSchedulesByDoctorId(doctorId: number): Observable<Schedule[]> {
    const headers = this.getHeaders();
    return this.http.get<Schedule[]>(`${API_URL}/Schedule/get-schedules-by-id/${doctorId}`, { headers });
  }

  // Thêm schedule
  createSchedule(schedule: Schedule): Observable<any> {
    const headers = this.getHeaders();
    return this.http.post(`${API_URL}/Schedule/Create-schedule`, schedule, { headers });
  }

  // Sửa schedule
  updateSchedule(id: number, schedule: Schedule): Observable<any> {
    const headers = this.getHeaders();
    return this.http.put(`${API_URL}/Schedule/edit-schedule-by-id/${id}`, schedule, { headers });
  }

  // Xóa schedule
  deleteSchedule(id: number): Observable<any> {
    const headers = this.getHeaders();
    return this.http.delete(`${API_URL}/Schedule/delete-schedule-by-id/${id}`, { headers });
  }
}