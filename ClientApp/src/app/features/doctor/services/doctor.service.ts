import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { API_URL } from '../../../app.config';
import { IonicModule } from '@ionic/angular';
import { Doctor } from '../list-doctor/models/doctor.model';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  

  constructor(private http: HttpClient) { }

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
  

