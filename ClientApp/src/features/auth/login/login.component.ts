import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoginData } from './Model/loginModel';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  @Output() switchForm = new EventEmitter<void>();

  loginData: LoginData = {
    email: '',
    password: ''
  };

  // constructor(private authService: AuthService) {}

  // async onSubmit() {
  //   await this.authService.login(this.loginData);
  // }

  // switchToRegister(event: Event) {
  //   event.preventDefault();
  //   this.switchForm.emit();
  // }
}