import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Clinic } from '../models/clinic.model';
import { API_URL } from '../../../app.config';

@Injectable({ providedIn: 'root' })
export class ClinicService {
  constructor(private http: HttpClient) {}

  getAllClinics(): Observable<Clinic[]> {
    return this.http.get<Clinic[]>(`${API_URL}/Clinic/get-all-clinics`);
  }

  getClinicById(id: number): Observable<Clinic> {
    return this.http.get<Clinic>(`${API_URL}/Clinic/getby/${id}`);
  }

  addClinic(clinic: Clinic): Observable<Clinic> {
    return this.http.post<Clinic>(`${API_URL}/Clinic/add`, clinic);
  }

  updateClinic(id: number, clinic: Clinic): Observable<Clinic> {
    return this.http.put<Clinic>(`${API_URL}/Clinic/update/${id}`, clinic);
  }

  deleteClinic(id: number): Observable<any> {
    return this.http.delete(`${API_URL}/Clinic/delete/${id}`);
  }
}
