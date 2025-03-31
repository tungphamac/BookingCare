<<<<<<< HEAD
import { Injectable } from '@angular/core';
=======
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TopDoctor } from '../models/top-doctor.model';
import { API_URL } from '../../../app.config';
import { TopRatingDoctor } from '../models/top-rating-doctor.model';
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

<<<<<<< HEAD
  constructor() { }
=======
  constructor(private http: HttpClient) { }

  getTopDoctors(): Observable<TopDoctor[]> {
    return this.http.get<TopDoctor[]>(`${API_URL}/Doctor/get-top-doctors`);
  }

  getTopRatingDoctors(): Observable<TopRatingDoctor[]> {
    return this.http.get<TopRatingDoctor[]>(`${API_URL}/Doctor/get-top-rating-doctors`);
  }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
}
