import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginRequest } from './Models/login-request.model';
import { AuthService } from '../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginRequest;

  constructor(private authService: AuthService, private cookieService: CookieService, private router: Router) {
    this.model = {
      email: '',
      password: ''
    }
  }

  onFormSubmit() {
    this.authService.login(this.model).subscribe({
      next: response => {
        console.log(response.token);

        this.cookieService.set('Authentication', `${response.token}`, undefined, '/', undefined, true, 'Strict');
        this.authService.setUser({ email: this.model.email });
        this.router.navigateByUrl('/');
      }
    });

  }

}
