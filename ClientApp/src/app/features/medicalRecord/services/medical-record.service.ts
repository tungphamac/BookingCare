import { UpdateMedicalRecordComponent } from './../components/update-medical-record/update-medical-record.component';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { MedicalRecordCreate } from '../models/medical-record-create.model';
import { MedicalRecordDetail } from '../models/medical-record-detail.model';

@Injectable({
  providedIn: 'root'
})
export class MedicalRecordService {
  private apiUrl = `${API_URL}/MedicalRecord`;

  constructor(private http: HttpClient) {}

  // Lấy header với token
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('Authentication');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  // Thêm hồ sơ y tế (Doctor)
  addMedicalRecord(record: MedicalRecordCreate): Observable<{ success: boolean; message: string; data: MedicalRecordDetail }> {
    return this.http.post<{ success: boolean; message: string; data: MedicalRecordDetail }>(this.apiUrl, record, { headers: this.getHeaders() });
  }

  // Cập nhật hồ sơ y tế (Doctor)
  updateMedicalRecord(id: number, record: MedicalRecordCreate): Observable<{ success: boolean; message: string; data: MedicalRecordDetail }> {
    return this.http.put<{ success: boolean; message: string; data: MedicalRecordDetail }>(`${this.apiUrl}/${id}`, record, { headers: this.getHeaders() });
  }

  // Xem hồ sơ y tế (Doctor, Patient)
  viewMedicalRecord(id: number): Observable<{ success: boolean; message: string; data: MedicalRecordDetail }> {
    return this.http.get<{ success: boolean; message: string; data: MedicalRecordDetail }>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }

  viewMedicalRecordByAppointment(appointmentId: number): Observable<{ success: boolean; message: string; data: MedicalRecordDetail }> {
    return this.http.get<{ success: boolean; message: string; data: MedicalRecordDetail }>(`${this.apiUrl}/by-appointment/${appointmentId}`,{ headers: this.getHeaders()});
  }

}