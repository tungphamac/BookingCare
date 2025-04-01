import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { User } from '../login/Models/user.model';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../login/Models/login-request.model';
import { LoginResponse } from '../login/Models/login-response.model';
import { API_URL } from '../../../app.config';
import { RegisterVm } from '../../register/Models/register.model';
import { resetPasswordVm } from '../../ResetPassword/Models/resetPass.model';
import { forgotPasswordVm } from '../../ForgotPassword/Models/forgot.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${API_URL}/Authentication/login`, request);
  }

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-id', user.id.toString()); // Lưu id
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const email = localStorage.getItem('user-email');
    const id = localStorage.getItem('user-id');

    if (email && id) {
      return {
        email: email,
        id: Number(id) // Chuyển id từ string sang number
      };
    }

    return undefined;
  }

  getToken(): string | null {
    return this.cookieService.get('Authentication');
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authentication', '/');
    this.$user.next(undefined);
  }

  forgotPassword(model: forgotPasswordVm): Observable<any> {
    return this.http.post<any>(`${API_URL}/Account/forgot-password`, model);
  }

  resetPassword(model: resetPasswordVm): Observable<any> {
    return this.http.post<any>(`${API_URL}/Account/reset-password`, model);
  }

  register(userData: RegisterVm): Observable<any> {
    return this.http.post<any>(`${API_URL}/Authentication/register`, userData).pipe(
      catchError(error => {
        console.error('Lỗi đăng ký:', error);
        let errorMessage = 'UserName Đã tồn tại';

        if (error.error) {
          if (error.error.message) errorMessage = error.error.message;
          if (error.error.errors) errorMessage += '\n' + error.error.errors.join('\n');
        }

        return throwError(() => new Error(errorMessage));
      })
    );
  }
}