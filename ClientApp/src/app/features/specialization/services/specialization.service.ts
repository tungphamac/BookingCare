<<<<<<< HEAD
import { Injectable } from '@angular/core';
=======
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopSpecialization } from '../models/top-specialization.model';
import { API_URL } from '../../../app.config';
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06

@Injectable({
  providedIn: 'root'
})
export class SpecializationService {

<<<<<<< HEAD
  constructor() { }
=======
  constructor(private http: HttpClient) { }

  getTopSpecializations(): Observable<TopSpecialization[]> {
    return this.http.get<TopSpecialization[]>(`${API_URL}/Specialization/get-top-specializations`);
  }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
}
