import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopDoctor } from '../models/top-doctor.model';
import { API_URL } from '../../../app.config';
import { TopRatingDoctor } from '../models/top-rating-doctor.model';
import { Doctor } from '../models/doctor.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private http: HttpClient) { }

  getTopDoctors(): Observable<TopDoctor[]> {
    return this.http.get<TopDoctor[]>(`${API_URL}/Doctor/get-top-doctors`);
  }

  getDoctorById(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(`${API_URL}/Doctor/get-doctor-by-id/${id}`);
  }

  getTopRatingDoctors(): Observable<TopRatingDoctor[]> {
    return this.http.get<TopRatingDoctor[]>(`${API_URL}/Doctor/get-top-rating-doctors`);
  }

  updateDoctorProfile(doctorId: number, formData: any): Observable<any> {

    // Gửi yêu cầu PUT đến API
    return this.http.put(`${API_URL}/Doctor/update-doctor-profile/${doctorId}`, formData);
  }
}
