import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { API_URL } from '../../../app.config';
import { PatientDetail } from '../models/patient-detail.model';
import { Patient } from '../models/patient.model';
import { Router } from '@angular/router';
import { ChangePasswordVm } from '../models/changepassword.model';
import { AuthService } from '../../auth/services/auth.service';



@Injectable({
  providedIn: 'root'
})
export class PatientService {
  constructor(private http: HttpClient, private authService: AuthService) { }

  // Lấy thông tin chi tiết bệnh nhân theo ID
  getPatientDetail(id: number): Observable<PatientDetail> {
    return this.http.get<PatientDetail>(`${API_URL}/Patient/get-patient-by-id/${id}`);
  }

  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${API_URL}/Patient/getall`);
  }

  getPatientById(id: number): Observable<Patient> {
    return this.http.get<Patient>(`${API_URL}/Patient/getby/${id}`);
  }

  addPatient(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(`${API_URL}/Patient/add`, patient);
  }
  updatePatient(id: number, patient: Patient): Observable<Patient> {
    return this.http.put<Patient>(`${API_URL}/Patient/update/${id}`, patient);
  }

  deletePatient(id: number): Observable<Patient> {
    return this.http.delete<Patient>(`${API_URL}/Patient/delete/${id}`);
  }
  changePassword(changePasswordVm: ChangePasswordVm): Observable<any> {
    const token = this.authService.getToken();
    if (!token) {
      return throwError(() => new Error('Không có token! Vui lòng đăng nhập lại.'));
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    return this.http.post(`${API_URL}/Account/change-password`, changePasswordVm, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Đã xảy ra lỗi không xác định';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Lỗi: ${error.error.message}`;
    } else {
      errorMessage = `Mã lỗi: ${error.status}\nThông báo: ${error.message}`;
    }
    return throwError(() => new Error(errorMessage));
  }
}
