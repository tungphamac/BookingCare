import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopRatingDoctor } from '../models/top-rating-doctor.model';
import { DoctorService } from '../services/doctor.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-top-rating-doctor-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './top-rating-doctor-list.component.html',
  styleUrl: './top-rating-doctor-list.component.css'
})
export class TopRatingDoctorListComponent implements OnInit {
  @ViewChild('doctorScroll', { static: false }) doctorScroll!: ElementRef;

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
  scrollLeft() {
    this.doctorScroll.nativeElement.scrollBy({ left: -960, behavior: 'smooth' }); // 3 card ~ 960px
  }

  scrollRight() {
    this.doctorScroll.nativeElement.scrollBy({ left: 960, behavior: 'smooth' });
  }

}
