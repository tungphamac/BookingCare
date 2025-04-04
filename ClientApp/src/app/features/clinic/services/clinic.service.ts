// src/app/features/clinic/services/clinic.service.ts
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../../app.config';
import { TopClinic } from '../models/top-clinic.model';
import { Clinic } from '../models/clinic.model';
import { ClinicDetailDto } from '../models/clinic-detail.model';
import { Doctor } from '../../doctor/models/doctor.model'; // Import Doctor model
import { DoctorDetailDto } from '../../doctor/models/doctor-detail.model';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {
  constructor(private http: HttpClient) { }

    addClinic(clinic: Clinic): Observable<Clinic> {
      return this.http.post<Clinic>(`${API_URL}/Clinic/add`, clinic);
    }
  
    updateClinic(id: number, clinic: Clinic): Observable<Clinic> {
      return this.http.put<Clinic>(`${API_URL}/Clinic/update/${id}`, clinic);
    }
  
    deleteClinic(id: number): Observable<any> {
      return this.http.delete(`${API_URL}/Clinic/delete/${id}`);
    }

  getAllClinics(): Observable<Clinic[]> {
    return this.http.get<Clinic[]>(`${API_URL}/Clinic/get-all-clinics`);
  }

  getTopClinics(): Observable<TopClinic[]> {
    return this.http.get<TopClinic[]>(`${API_URL}/Clinic/get-top-clinic`);
  }

  getClinicById(id: number): Observable<Clinic> {
    return this.http.get<Clinic>(`${API_URL}/Clinic/get-clinic-by-id/${id}`);
  }

  getClinicBySpecializationId(specializationId: number): Observable<ClinicDetailDto[]> {
    return this.http.get<ClinicDetailDto[]>(`${API_URL}/Clinic/get-clinics-by-specialization/${specializationId}`);
  }

  // Thêm phương thức mới để lấy danh sách bác sĩ theo clinicId
  getDoctorsByClinicId(clinicId: number): Observable<DoctorDetailDto[]> {
    return this.http.get<DoctorDetailDto[]>(`${API_URL}/Clinic/get-doctors-by-clinic-id/${clinicId}`);
  }
}