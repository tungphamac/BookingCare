import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorService } from '../services/doctor.service';
import { RouterEvent, RouterLink } from '@angular/router';

@Component({
  selector: 'app-top-doctor-list',
  imports: [CommonModule,RouterLink],
  templateUrl: './top-doctor-list.component.html',
  styleUrl: './top-doctor-list.component.css'
})
export class TopDoctorListComponent implements OnInit {
  topDoctors: any[] = [];

  constructor(private doctorService: DoctorService) { }

  ngOnInit(): void {
    this.doctorService.getTopDoctors().subscribe({
      next: response => {
        this.topDoctors = response;
        console.log(this.topDoctors);
      }
    });
  }
}
