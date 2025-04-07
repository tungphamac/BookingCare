// src/app/services/doctor.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Doctor } from '../model/doctor.model';


@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  private apiUrl = 'https://localhost:7182/api/Doctor';  // Adjust API URL accordingly

  constructor(private http: HttpClient) {}

  // Get all doctors
  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.apiUrl}/getall`);
  }

  // Get doctor by ID
  getDoctorById(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(`${this.apiUrl}/get-doctor-by-id/${id}`);
  }
  addDoctor(doctor: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/add_doctor`, doctor);
  }
  // Create new doctor
  

  // Update existing doctor
  
  updateDoctor(id: number, doctor: Doctor): Observable<Doctor> {
    return this.http.put<Doctor>(`${this.apiUrl}/update/${id}`, doctor);
  }
  

  // Delete doctor
  deleteDoctor(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/delete/${id}`);
  }
}
