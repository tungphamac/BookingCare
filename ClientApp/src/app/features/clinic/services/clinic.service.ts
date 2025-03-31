import { Injectable } from '@angular/core';
<<<<<<< HEAD
=======
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../../app.config';
import { TopClinic } from '../models/top-clinic.model';
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06

@Injectable({
  providedIn: 'root'
})
export class ClinicService {

<<<<<<< HEAD
  constructor() { }
=======
  constructor(private http: HttpClient) { }

  getTopClinics(): Observable<TopClinic[]> {
    return this.http.get<TopClinic[]>(`${API_URL}/Clinic/get-top-clinic`);
  }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
}
