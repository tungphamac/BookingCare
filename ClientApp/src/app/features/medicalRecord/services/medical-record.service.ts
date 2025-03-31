import { Injectable } from '@angular/core';
<<<<<<< HEAD
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { MedicalRecordCreate } from '../models/medical-record-create.model';
import { MedicalRecordDetail } from '../models/medical-record-detail.model';
=======
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06

@Injectable({
  providedIn: 'root'
})
export class MedicalRecordService {
<<<<<<< HEAD
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
  addMedicalRecord(record: MedicalRecordCreate): Observable<any> {
    return this.http.post<any>(this.apiUrl, record, { headers: this.getHeaders() });
  }

  // Cập nhật hồ sơ y tế (Doctor)
  updateMedicalRecord(id: number, record: MedicalRecordCreate): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, record, { headers: this.getHeaders() });
  }

  // Xem hồ sơ y tế (Doctor, Patient)
  viewMedicalRecord(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
}
=======

  constructor() { }
}
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
