import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserDetailsVm } from './model/user-details.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7182/api/Account'; // Đổi thành URL của API backend của bạn

  constructor(private http: HttpClient) { }

  // Lấy tất cả người dùng
  getAllUsers(): Observable<UserDetailsVm[]> {
    return this.http.get<UserDetailsVm[]>(`${this.apiUrl}/getall`);
  }

  // Lấy thông tin người dùng theo userId
  getUserById(userId: number): Observable<UserDetailsVm> {
    return this.http.get<UserDetailsVm>(`${this.apiUrl}/getby/${userId}`);
  }

  // Khóa tài khoản người dùng
  lockUserAccount(userId: number, duration: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'  // Đảm bảo header là application/json
    });
    
    // Gửi request với header Content-Type là application/json
    return this.http.post<any>(`${this.apiUrl}/lock/${userId}`, JSON.stringify(duration), { headers });
  }
  // Mở khóa tài khoản người dùng
  unlockUserAccount(userId: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/unlock/${userId}`, {});
  }
}
