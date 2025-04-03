import { Component } from '@angular/core';
import { DoctorService } from '../services/doctor.service';
import { Router } from '@angular/router';
import { CreateDoctorDto, Doctor } from '../list-doctor/models/doctor.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-doctor',
  standalone: true,  // Đảm bảo component là standalone
  imports: [FormsModule],  // Import FormsModule trong standalone component
  templateUrl: './add-doctor.component.html',
  styleUrls: ['./add-doctor.component.css']
})
export class AddDoctorComponent {
  newDoctor: CreateDoctorDto = new CreateDoctorDto(
    '',    // userName
    '',    // email
    '',    // password
    true,  // gender (default is Male)
    '',    // address
    '',    // avatar
    '',    // achievement
    '',    // description
    0,     // specializationId
    0      // clinicId
  );


  constructor(private doctorService: DoctorService, private router: Router) { }
  goBack() {
    this.router.navigate(['/Doctor/getall']);
  }
  
  addDoctor() {
    this.doctorService.addDoctor(this.newDoctor).subscribe({
      next: () => {
        // Chuyển về trang danh sách bác sĩ sau khi thêm thành công
        this.router.navigate(['/Doctor/getall']);
      },
      error: (err) => {
        console.error('Error adding doctor', err);
      }
    });
  }
}
