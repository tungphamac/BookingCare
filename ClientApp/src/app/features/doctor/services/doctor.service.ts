import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopDoctor } from '../models/top-doctor.model';
import { API_URL } from '../../../app.config';
import { TopRatingDoctor } from '../models/top-rating-doctor.model';
import { Doctor, GetDoctor } from '../models/doctor.model';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { DoctorDetailDto } from '../models/doctor-detail.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private http: HttpClient) { }

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getTopDoctors(): Observable<TopDoctor[]> {
    return this.http.get<TopDoctor[]>(`${API_URL}/Doctor/get-top-doctors`);
  }

  getDoctorById(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(`${API_URL}/Doctor/get-doctor-by-id/${id}`, { headers: this.getHeaders() });
  }

  getTopRatingDoctors(): Observable<TopRatingDoctor[]> {
    return this.http.get<TopRatingDoctor[]>(`${API_URL}/Doctor/get-top-rating-doctors`);
  }

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${API_URL}/Doctor/getall`);
  }


  deleteDoctor(userId: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/Doctor/delete/${userId}`);
  }

  updateDoctor(id: number, doctor: Doctor): Observable<Doctor> {
    return this.http.put<Doctor>(`${API_URL}/Doctor/update/${id}`, doctor);
  }

  updateDoctorProfile(doctorId: number, formData: any): Observable<any> {
    return this.http.put(`${API_URL}/Doctor/update-doctor-profile/${doctorId}`, formData);
  }

  lockDoctorAccount(id: number, lockUntil: Date | null): Observable<any> {
    const params = new HttpParams().set('lockUntil', lockUntil?.toISOString() || '');
    return this.http.post<any>(`${API_URL}/Doctor/lock/${id}`, null, { params });
  }


  // Thêm phương thức mới để gọi API lấy bác sĩ theo specializationId và clinicId
  getDoctorsBySpecializationAndClinic(specializationId: number, clinicId: number): Observable<DoctorDetailDto[]> {
    const params = new HttpParams()
      .set('specializationId', specializationId.toString())
      .set('clinicId', clinicId.toString());
    return this.http.get<DoctorDetailDto[]>(`${API_URL}/Doctor/get-doctors-by-specialization-and-clinic`, { params });

  }

  getClinicById(clinicId: number): Observable<{ id: number; name: string }> {
    return this.http.get<{ id: number; name: string }>(
      `${API_URL}/Clinic/get-clinic-by-id/${clinicId}`,
      { headers: this.getHeaders() }
    );
  }



}
