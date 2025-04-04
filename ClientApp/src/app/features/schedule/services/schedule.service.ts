  import { Injectable } from '@angular/core';
  import { HttpClient } from '@angular/common/http';
  import { Observable } from 'rxjs';
  import { API_URL } from '../../../app.config';
  import { Schedule } from '../models/schedule.model';

  @Injectable({
    providedIn: 'root'
  })
  export class ScheduleService {
    constructor(private http: HttpClient) {}

    // Xem chi tiết schedule theo ID
    getScheduleById(id: number): Observable<Schedule> {
      return this.http.get<Schedule>(`${API_URL}/Schedule/get-schedule-by-id/${id}`);
    }

    // Xem danh sách tất cả schedules
    getAllSchedules(): Observable<Schedule[]> {
      return this.http.get<Schedule[]>(`${API_URL}/Schedule`);
    }

    // Xem danh sách schedules theo doctorId
    getSchedulesByDoctorId(doctorId: number): Observable<Schedule[]> {
      return this.http.get<Schedule[]>(`${API_URL}/Schedule/get-schedules-by-id/${doctorId}`);
    }

    // Thêm schedule
    createSchedule(schedule: Schedule): Observable<any> {
      return this.http.post(`${API_URL}/Schedule/Create-schedule-by-doctor`, schedule);
    }

    // Sửa schedule
    updateSchedule(id: number, schedule: Schedule): Observable<any> {
      return this.http.put(`${API_URL}/Schedule/edit-schedule-by-id/${id}`, schedule);
    }

    // Xóa schedule
    deleteSchedule(id: number): Observable<any> {
      return this.http.delete(`${API_URL}/Schedule/delete-schedule-by-id/${id}`);
    }
  }