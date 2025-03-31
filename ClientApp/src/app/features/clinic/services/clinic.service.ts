import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../../app.config';
import { TopClinic } from '../models/top-clinic.model';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {

  constructor(private http: HttpClient) { }

  getTopClinics(): Observable<TopClinic[]> {
    return this.http.get<TopClinic[]>(`${API_URL}/Clinic/get-top-clinic`);
  }
}
