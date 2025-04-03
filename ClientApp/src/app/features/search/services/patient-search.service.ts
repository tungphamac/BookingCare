import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { SearchResult } from '../models/search-result.model';

@Injectable({
  providedIn: 'root'
})
export class PatientSearchService {
  constructor(private http: HttpClient) {}

  // Tìm kiếm bệnh nhân cho bác sĩ
  searchPatientsForDoctor(doctorId: number, keyword: string): Observable<SearchResult> {
    return this.http.get<SearchResult>(`${API_URL}/Search/Patients?doctorId=${doctorId}&keyword=${keyword}`);
  }
}