import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-doctor-profile',
  imports: [CommonModule, FormsModule],
  templateUrl: './doctor-profile.component.html',
  styleUrl: './doctor-profile.component.css'
})
export class DoctorProfileComponent {
  doctor: any = {
    name: '',
    gender: true,
    password: '',
    email: '',
    phone: '',
    address: '',
    avatar: '',
    achievement: '',
    description: '',
    specializationId: 0,
    clinicId: 0
  };

  onSubmit() {
    console.log('Saving doctor profile:', this.doctor);
    // Here you would typically make an API call to save the doctor's profile
    alert('Profile saved successfully!');
  }
  changeAvatar() {
    const newAvatar = prompt('Enter new avatar URL:');
    if (newAvatar) {
      this.doctor.avatar = newAvatar;
    }
  }
}
