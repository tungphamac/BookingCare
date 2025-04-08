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

  getFeedbacks(): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${API_URL}/Feedback/get-all-feedbacks`);
  }

  getFeedbackById(id: number): Observable<Feedback> {
    return this.http.get<Feedback>(`${API_URL}/Feedback/get-feedback-by-id/${id}`);
  }

  addFeedback(feedback: Feedback): Observable<any> {
    const token = localStorage.getItem('token'); // Hoặc sessionStorage nếu bạn lưu ở đó
    const headers = {
      'Authorization': `Bearer ${token}`
    };
    return this.http.post(`${API_URL}/Feedback/add-feedback`, feedback, { headers });
  }

  deleteFeedback(id: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/Feedback/delete-feedback-by-id/${id}`);
  }

  getFeedbacksByDoctor(doctorId: number): Observable<Feedback[]> {
    if (!doctorId) {
      console.error('Doctor ID is invalid for API call!');
      throw new Error('Doctor ID is required');
    }
    return this.http.get<Feedback[]>(`${API_URL}/Feedback/get-feedbacks-by-doctorId/${doctorId}`);
  }

  getFeedbackByAppointment(appointmentId: number): Observable<Feedback> {
    return this.http.get<Feedback>(`${API_URL}/Feedback/get-feedback-by-appointment/${appointmentId}`);
  }
}
