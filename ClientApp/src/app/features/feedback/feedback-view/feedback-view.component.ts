import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Feedback } from '../models/feedback';
import { FeedbackService } from '../services/feedback.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-feedback-view',
  imports: [CommonModule],
  templateUrl: './feedback-view.component.html',
  styleUrl: './feedback-view.component.css'
})
export class FeedbackViewComponent implements OnInit {
  feedbackList: Feedback[] = [];
  doctorId: number = 0;

  constructor(private feedbackService: FeedbackService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.doctorId = +params['doctorId'];
    });
    this.feedbackService.getFeedbacksByDoctor(this.doctorId).subscribe({
      next: response => {
        this.feedbackList = response;
        console.log(this.feedbackList);
      }
    });
  }
}
