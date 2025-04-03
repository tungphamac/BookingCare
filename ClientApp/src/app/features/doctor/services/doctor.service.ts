
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopDoctor } from '../models/top-doctor.model';
import { API_URL } from '../../../app.config';
import { TopRatingDoctor } from '../models/top-rating-doctor.model';
import { Doctor } from '../models/doctor.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IonicModule } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private http: HttpClient) { }
  

  getTopDoctors(): Observable<TopDoctor[]> {
    return this.http.get<TopDoctor[]>(`${API_URL}/Doctor/get-top-doctors`);
  }

  getTopRatingDoctors(): Observable<TopRatingDoctor[]> {
    return this.http.get<TopRatingDoctor[]>(`${API_URL}/Doctor/get-top-rating-doctors`);
  }

  getDoctorById(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(`${API_URL}/Doctor/get-doctor-by-id/${id}`);
  }

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${API_URL}/Doctor/getall`);
  
  }
  addDoctor(doctor: Doctor): Observable<Doctor> {
    return this.http.post<Doctor>(`${API_URL}/Doctor/add`, doctor);
  }
   // Xóa bác sĩ từ backend
   deleteDoctor(userId: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/Doctor/delete${userId}`);
  }
  updateDoctor(id: number, doctor: Doctor): Observable<Doctor> {
    return this.http.put<Doctor>(`${API_URL}/Doctor/update${id}`, doctor);

}
lockDoctorAccount(id: number, lockUntil: Date | null): Observable<any> {
  const params = new HttpParams().set('lockUntil', lockUntil?.toISOString() || '');
  return this.http.post<any>(`${API_URL}/Doctor/lock/${id}`, null, { params });
}

}
  

