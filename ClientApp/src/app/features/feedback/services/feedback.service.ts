<<<<<<< HEAD
import { Injectable } from '@angular/core';
=======
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feedback } from '../models/feedback';
import { API_URL } from '../../../app.config';
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

<<<<<<< HEAD
  constructor() { }
=======
  constructor(private http: HttpClient) { }

  getFeedbacks(): Observable<{ message: string; data: Feedback[] }> {
    return this.http.get<{ message: string; data: Feedback[] }>(`${API_URL}/Feedback/get-all-feedbacks`);
  }

  getFeedbackById(id: number): Observable<Feedback> {
    return this.http.get<Feedback>(`${API_URL}/Feedback/get-feedback-by-id/${id}`);
  }

  addFeedback(feedback: Feedback): Observable<Feedback> {
    return this.http.post<Feedback>(`${API_URL}/Feedback/add-feedback`, feedback);
  }

  deleteFeedback(id: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/Feedback/delete-feedback-by-id/${id}`);
  }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
}
