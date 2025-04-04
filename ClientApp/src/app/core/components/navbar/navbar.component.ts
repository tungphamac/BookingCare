
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { User } from '../../../features/auth/login/Models/user.model';
import { AuthService } from '../../../features/auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [FormsModule, RouterLink, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent implements OnInit {
  user?: User;

  constructor(private authService: AuthService, private router: Router) {

  }
  ngOnInit(): void {
    this.authService.user().subscribe(
      {
        next: response => {

          this.user = response;
        }
      }
    );

    this.user = this.authService.getUser();
  }

  onLogout() {
    this.authService.logout();
  }
  onNavigateToProfile(): void {
    if (this.user?.role === 'Patient') {
        this.router.navigate(['/patients', this.user.id]); // Điều hướng đến trang bệnh nhân
    } else if (this.user?.role === 'Doctor') {
        this.router.navigate(['/doctor-profile', this.user.id]); // Điều hướng đến trang bác sĩ
    } else {
        alert('Vai trò không xác định!');
    }
}
}
