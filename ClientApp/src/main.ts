import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './features/auth/login/login.component';
// import { RegisterComponent } from './app/components/register/register.component';

@Component({
  selector: 'app-root',
  template: `
    <div class="container">
      <app-login *ngIf="isLoginForm" (switchForm)="toggleForm()"></app-login>
      <!-- <app-register *ngIf="!isLoginForm" (switchForm)="toggleForm()"></app-register> -->
    </div>
  `,
  standalone: true,
  imports: [CommonModule, LoginComponent]//RegisterComponent],
})
export class App {
  isLoginForm = true;

  toggleForm() {
    this.isLoginForm = !this.isLoginForm;
  }
}

import { bootstrapApplication } from '@angular/platform-browser';

bootstrapApplication(App, {
  providers: []
}).catch(err => console.error(err));