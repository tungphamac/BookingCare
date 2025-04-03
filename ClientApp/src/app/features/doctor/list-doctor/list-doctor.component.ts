import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Doctor } from './models/doctor.model';
import { DoctorService } from '../services/doctor.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-doctor',
  imports: [CommonModule, RouterLink],
  templateUrl: './list-doctor.component.html',
  styleUrls: ['./list-doctor.component.css']
})
export class ListDoctorComponent implements OnInit {
  doctors: Doctor[] = [];
  selectedDoctor: Doctor | null = null;
  @ViewChild('userId')
  userId!: ElementRef;
  
  constructor(private doctorService: DoctorService) { }
  ngOnInit(): void {
    this.loadDoctors();
  }
  loadDoctors(): void {
    this.doctorService.getDoctors().subscribe({
      next: (data) => {
        this.doctors = data;
      },
      error: (error) => {
        console.error('There was an error!', error);
      }
    });
  }
  deleteDoctor(userId: number | undefined) {
    // Kiểm tra nếu userId là undefined, không gọi API
    if (userId === undefined) {
      console.error('Invalid userId');
      return;
    }
  
    // Tiến hành gọi API để xóa bác sĩ
    this.doctorService.deleteDoctor(userId).subscribe({
      next: () => {
        // Sau khi xóa thành công từ backend, cập nhật lại danh sách bác sĩ
        this.doctors = this.doctors.filter(doctor => doctor.userId !== userId);
      },
      error: (error) => {
        console.error('Error deleting doctor:', error);
      }
    });
  }  
  addDoctor(): void {
    const newDoctor: Doctor = {
      userId: this.userId.nativeElement.value,
      // Set other properties similarly
    };
    
    this.doctorService.addDoctor(newDoctor).subscribe({
      next: (doctor) => {
        this.doctors.push(doctor);
        // Reset fields or close modal here if needed
      },
      error: (error) => console.error('Failed to add doctor:', error)
    });
  }
  editDoctor(doctor: Doctor): void {
    this.selectedDoctor = { ...doctor };
  }
 
  
  
}
