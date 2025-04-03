import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { PatientDetail } from '../models/patient-detail.model';
import { Patient } from '../models/patient.model';
import { Router } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class PatientService {
  constructor(private http: HttpClient) {}

  // Lấy thông tin chi tiết bệnh nhân theo ID
  getPatientDetail(id: number): Observable<PatientDetail> {
    return this.http.get<PatientDetail>(`${API_URL}/Patient/get-patient-by-id/${id}`);
  }

  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${API_URL}/Patient/getall`);
  }
  
  getPatientById(id: number): Observable<Patient> {
    return this.http.get<Patient>(`${API_URL}/Patient/getby/${id}`);
  }
  
  addPatient(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(`${API_URL}/Patient/add`, patient);
  }
  updatePatient(id: number, patient: Patient): Observable<Patient> {
    return this.http.put<Patient>(`${API_URL}/Patient/update/${id}`, patient);
  }

  deletePatient(id: number): Observable<Patient> {
    return this.http.delete<Patient>(`${API_URL}/Patient/delete/${id}`);
  }
}
