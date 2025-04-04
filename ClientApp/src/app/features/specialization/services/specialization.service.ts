import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopSpecialization } from '../models/top-specialization.model';
import { API_URL } from '../../../app.config';
import { Specialization } from '../models/specialization.model';
import { DoctorDetailDto } from '../../doctor/models/doctor-detail.model';

@Injectable({
  providedIn: 'root'
})
export class SpecializationService {

  constructor(private http: HttpClient) { }

  getAllSpecializations(): Observable<Specialization[]> {
    return this.http.get<Specialization[]>(`${API_URL}/Specialization/get-all-specializations`);
  }

  getTopSpecializations(): Observable<TopSpecialization[]> {
    return this.http.get<TopSpecialization[]>(`${API_URL}/Specialization/get-top-specializations`);
  }

  getSpecializationById(id: number): Observable<Specialization> {
    return this.http.get<Specialization>(`${API_URL}/Specialization/get-specialization-by-id/${id}`);
  }

  getDoctorsBySpecializationId(specializationId: number): Observable<DoctorDetailDto[]> {
    return this.http.get<DoctorDetailDto[]>(`${API_URL}/Doctor/get-doctors-by-specialization/${specializationId}`); // Sửa endpoint
  }

  getSpecializationsByClinicId(clinicId: number): Observable<Specialization[]> {
    return this.http.get<Specialization[]>(`${API_URL}/Specialization/clinic/${clinicId}`);
  }
}