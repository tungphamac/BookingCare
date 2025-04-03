import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Specialization } from '../models/specialization.model';
import { API_URL } from '../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class SpecializationService {
  private apiUrl = 'https://localhost:7182/api/Specialization';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Specialization[]> {
    return this.http.get<Specialization[]>(`${this.apiUrl}/getall`);
  }

  getById(id: number): Observable<Specialization> {
    return this.http.get<Specialization>(`${this.apiUrl}/getby/${id}`);
  }

  create(data: Specialization): Observable<Specialization> {
    return this.http.post<Specialization>(`${this.apiUrl}/add`, data);
  }

  update(id: number, data: Specialization): Observable<Specialization> {
    return this.http.put<Specialization>(`${this.apiUrl}/update/${id}`, data);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete/${id}`);
  }
  uploadSpecialization(formData: FormData): Observable<any> {
    return this.http.post(`${API_URL}/Specialization/upload`, formData);
  }
  
  importSpecializations(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/import`, formData);
  }
}
