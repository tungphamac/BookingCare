
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopSpecialization } from '../models/top-specialization.model';
import { API_URL } from '../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class SpecializationService {


  constructor(private http: HttpClient) { }

  getTopSpecializations(): Observable<TopSpecialization[]> {
    return this.http.get<TopSpecialization[]>(`${API_URL}/Specialization/get-top-specializations`);
  }
}
