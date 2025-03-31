import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopRatingDoctor } from '../models/top-rating-doctor.model';
import { DoctorService } from '../services/doctor.service';

@Component({
  selector: 'app-top-rating-doctor-list',
  imports: [CommonModule],
  templateUrl: './top-rating-doctor-list.component.html',
  styleUrl: './top-rating-doctor-list.component.css'
})
export class TopRatingDoctorListComponent implements OnInit {
  topRatingDoctors: TopRatingDoctor[] = [];

  constructor(private doctorService: DoctorService) { }

  ngOnInit(): void {
    this.doctorService.getTopRatingDoctors().subscribe({
      next: response => {
        this.topRatingDoctors = response;
        console.log(this.topRatingDoctors);
      }
    });
  }
}
