import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { PatientDetail } from '../models/patient-detail.model';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  constructor(private http: HttpClient) {}

  // Lấy thông tin chi tiết bệnh nhân theo ID
  getPatientDetail(id: number): Observable<PatientDetail> {
    return this.http.get<PatientDetail>(`${API_URL}/Patient/get-patient-by-id/${id}`);
  }
}