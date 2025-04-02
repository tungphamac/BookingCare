import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../../app.config';
import { TopClinic } from '../models/top-clinic.model';
import { Clinic } from '../models/clinic.model';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {

  constructor(private http: HttpClient) { }

  getAllClinics(): Observable<Clinic[]> {
    return this.http.get<Clinic[]>(`${API_URL}/Clinic/get-all-clinics`);
  }

  getTopClinics(): Observable<TopClinic[]> {
    return this.http.get<TopClinic[]>(`${API_URL}/Clinic/get-top-clinic`);
  }


}
