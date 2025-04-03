import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopSpecialization } from '../models/top-specialization.model';
import { API_URL } from '../../../app.config';
import { SpecializationDetailDto } from '../models/specialization-detail.model';
import { DoctorDetailDto } from '../../doctor/models/doctor-detail.model';

@Injectable({
  providedIn: 'root'
})
export class SpecializationService {

  constructor(private http: HttpClient) { }

  getTopSpecializations(): Observable<TopSpecialization[]> {
    return this.http.get<TopSpecialization[]>(`${API_URL}/Specialization/get-top-specializations`);
  }

  getAllSpecializations(): Observable<SpecializationDetailDto[]> {
    return this.http.get<SpecializationDetailDto[]>(`${API_URL}/Specialization/get-all-specializations`);
  }

  getSpecializationById(id: number): Observable<SpecializationDetailDto> {
    return this.http.get<SpecializationDetailDto>(`${API_URL}/Specialization/get-specialization-by-id/${id}`);
  }

  getDoctorsBySpecializationId(specializationId: number): Observable<DoctorDetailDto[]> {
    return this.http.get<DoctorDetailDto[]>(`${API_URL}/Doctor/get-doctors-by-specialization/${specializationId}`); // Sửa endpoint
  }
}