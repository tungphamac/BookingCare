import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { SearchResult } from '../models/search-result.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  constructor(private http: HttpClient) {}

  // Tìm kiếm chung (Doctor, Clinic, Specialization)
  generalSearch(filter: string, keyword: string): Observable<SearchResult> {
    return this.http.get<SearchResult>(`${API_URL}/Search?filter=${filter}&keyword=${keyword}`);
  }

  // Tìm kiếm theo chuyên khoa (Specialization)
  searchBySpecialization(keyword: string): Observable<SearchResult> {
    return this.http.get<SearchResult>(`${API_URL}/Search/Specialization?keyword=${keyword}`);
  }

  searchPatients(doctorId: number, keyword: string): Observable<SearchResult> {
    return this.http.get<SearchResult>(`${API_URL}/Search/Patients?doctorId=${doctorId}&keyword=${keyword}`);
  }
}