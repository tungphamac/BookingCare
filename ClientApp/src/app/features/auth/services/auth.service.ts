import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { User } from '../login/Models/user.model';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../login/Models/login-request.model';
import { LoginResponse } from '../login/Models/login-response.model';
import { API_URL } from '../../../app.config';
import { RegisterVm } from '../../register/Models/register.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<User | undefined>(undefined);
  constructor(private http: HttpClient, private cookieService: CookieService) { }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${API_URL}/Authentication/login`, request);
  }

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const email = localStorage.getItem("user-email");

    if (email) {
      return {
        email: email
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
  register(userData: RegisterVm): Observable<any> {
    return this.http.post<any>(`${API_URL}/Authentication/register`, userData)
      .pipe(
        catchError(error => {
          console.error('Register error:', error);
          return throwError(() => new Error(error.message));
        })
      );

  }
}
