import { GetDoctor } from './../models/doctor.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DoctorService } from '../services/doctor.service';
import { Doctor } from '../models/doctor.model';
import { Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ScheduleService } from '../../schedule/services/schedule.service';
import { Schedule } from '../../schedule/models/schedule.model';
import { Feedback } from '../../feedback/models/feedback';
import { FeedbackService } from '../../feedback/services/feedback.service';
import { FeedbackViewComponent } from "../../feedback/feedback-view/feedback-view.component";

@Component({
  selector: 'app-doctor-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, FeedbackViewComponent],
  templateUrl: './doctorc-detail.component.html',
  styleUrl: './doctorc-detail.component.css'
})
export class DoctorDetailComponent implements OnInit {
  doctor: Doctor | null = null;
  schedules: Schedule[] = [];
  feedbacks: Feedback[] = [];
  errorMessage: string | null = null;
  isLoadingSchedules: boolean = false;
  isLoadingFeedbacks: boolean = false;
  
  constructor(
    private route: ActivatedRoute,
    private doctorService: DoctorService,
    private scheduleService: ScheduleService,
    private feedbackService: FeedbackService,
    private location: Location
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : null;
    if (id !== null && !isNaN(id)) {
      this.loadDoctorDetails(id);
      this.loadDoctorSchedules(id);
      this.loadDoctorFeedbacks(id);
    } else {
      this.errorMessage = 'Invalid doctor ID.';
    }
  }

  loadDoctorDetails(id: number): void {
    this.doctorService.getDoctorById(id).subscribe({
      next: (data: Doctor) => {
        this.doctor = data;
        console.log('Doctor details loaded:', this.doctor); // Debug log
        console.log('Doctor ID:', this.doctor?.id); // Check doctor.userId
      },
      error: (err) => {
        if (err.status === 404) {
          this.errorMessage = 'Doctor not found.';
        } else {
          this.errorMessage = 'Failed to load doctor details. Please try again later.';
        }
        console.error(err);
      }
    });
  }

  loadDoctorSchedules(id: number): void {
    this.isLoadingSchedules = true;
    this.scheduleService.getSchedulesByDoctorId(id).subscribe({
      next: (schedules) => {
        this.schedules = schedules;
        this.isLoadingSchedules = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load doctor schedules.';
        this.isLoadingSchedules = false;
        console.error(err);
      }
    });
  }

  loadDoctorFeedbacks(doctorId: number): void {
    this.isLoadingFeedbacks = true;
    this.feedbackService.getFeedbacksByDoctor(doctorId).subscribe({
      next: (data) => {
        this.feedbacks = data;
        this.isLoadingFeedbacks = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load feedbacks.';
        this.isLoadingFeedbacks = false;
        console.error(err);
      }
    });
  }

  goBack(): void {
    this.location.back();
  }
  
}