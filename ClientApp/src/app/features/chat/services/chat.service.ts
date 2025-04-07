// src/app/features/chat/services/chat.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../app.config';
import { AuthService } from '../../auth/services/auth.service';
import { Message } from '../models/message.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  sendMessage(senderId: number, receiverId: number, content: string): Observable<any> {
    const body = { senderId, receiverId, content };
    return this.http.post(`${API_URL}/Chat/send`, body, { headers: this.getHeaders() });
  }

  getChatHistory(userId1: number, userId2: number): Observable<{ data: Message[] }> {
    return this.http.get<{ data: Message[] }>(`${API_URL}/Chat/history?userId1=${userId1}&userId2=${userId2}`, { headers: this.getHeaders() });
  }

  markMessageAsRead(messageId: number): Observable<any> {
    return this.http.put(`${API_URL}/Chat/read?messageId=${messageId}`, null, { headers: this.getHeaders() });
  }

  getUnreadMessages(): Observable<{ data: Message[] }> {
    return this.http.get<{ data: Message[] }>(`${API_URL}/Chat/unread?userId=${this.authService.getUser()?.id}`, { headers: this.getHeaders() });
  }

  getChatParticipants(): Observable<{ data: number[] }> {
    return this.http.get<{ data: number[] }>(`${API_URL}/Chat/participants`, { headers: this.getHeaders() });
  }
  getUserInfo(userId: number): Observable<{ data: User }> {
    return this.http.get<{ data: User }>(`${API_URL}/Chat/user/${userId}`, { headers: this.getHeaders() });
  }
}