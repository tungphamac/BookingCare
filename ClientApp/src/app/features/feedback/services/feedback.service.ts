import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feedback } from '../models/feedback';
import { API_URL } from '../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http: HttpClient) { }

  getFeedbacks(): Observable<{ message: string; data: Feedback[] }> {
    return this.http.get<{ message: string; data: Feedback[] }>(`${API_URL}/Feedback/get-all-feedbacks`);
  }

  getFeedbackById(id: number): Observable<Feedback> {
    return this.http.get<Feedback>(`${API_URL}/Feedback/get-feedback-by-id/${id}`);
  }

  addFeedback(feedback: Feedback): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${API_URL}/Feedback/add-feedback`, feedback);
  }

  deleteFeedback(id: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/Feedback/delete-feedback-by-id/${id}`);
  }

  getFeedbacksByDoctor(doctorId: number): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${API_URL}/Feedback/get-feedbacks-by-doctorId/${doctorId}`);
  }

  getFeedbackByAppointment(appointmentId: number): Observable<Feedback> {
    return this.http.get<Feedback>(`${API_URL}/Feedback/get-feedback-by-appointment/${appointmentId}`);
  }
}
