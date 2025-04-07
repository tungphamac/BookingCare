import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { User } from '../login/Models/user.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../login/Models/login-request.model';
import { LoginResponse } from '../login/Models/login-response.model';
import { API_URL } from '../../../app.config';
import { RegisterVm } from '../../register/Models/register.model';
import { forgotPasswordVm } from '../../ForgotPassword/Models/forgot.model';

import { resetPasswordVm } from '../../ResetPassword/Models/resetPass.model';

import { tap } from 'rxjs/operators';
import { ChangePasswordVm } from '../../patient/models/changepassword.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<User | undefined>(undefined);
  constructor(private http: HttpClient, private cookieService: CookieService) { }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${API_URL}/Authentication/login`, request).pipe(
      tap((response: LoginResponse) => {
        this.$user.next({ email: response.email, id: response.id, role: response.role }); // Lưu role
        localStorage.setItem('user-id', response.id.toString());
        localStorage.setItem('user-email', response.email);
        localStorage.setItem('user-role', response.role);
        localStorage.setItem('token', response.token); // Lưu role vào localStorage
      })
    );
  }
  // Trong AuthService

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-namename', user.email);
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUser(): User | undefined {
    const email = localStorage.getItem("user-email");
    const id = localStorage.getItem("user-id");
    const role = localStorage.getItem("user-role");

    if (email && id && role) {
      return {
        email: email,
        id: +id,
        role: role
      };
    }

    return undefined;
  }
  logout(): void {
    //localStorage.removeItem("user-email");
    localStorage.clear();
    this.cookieService.delete("Authentication", "/");
    this.$user.next(undefined);
  }
  forgotPassword(model: forgotPasswordVm): Observable<any> {
    return this.http.post<any>(`${API_URL}/Account/forgot-password`, model);
  }

  resetPassword(model: resetPasswordVm): Observable<any> {
    return this.http.post<any>(`${API_URL}/Account/reset-password`, model);
  }

  getUserById(id: string): Observable<any> {
    return this.http.get(`${API_URL}/Patient/${id}`);
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

