
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopSpecialization } from '../models/top-specialization.model';
import { API_URL } from '../../../app.config';
import { Specialization } from '../models/specialization.model';

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
}
