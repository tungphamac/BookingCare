import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Feedback } from '../models/feedback';
import { FeedbackService } from '../services/feedback.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-feedback-add',
  imports: [CommonModule, FormsModule],
  templateUrl: './feedback-add.component.html',
  styleUrl: './feedback-add.component.css'
})
export class FeedbackAddComponent implements OnInit {
  feedback: Feedback;

  constructor(private feedbackService: FeedbackService) {
    this.feedback = { id: 0, appointmentId: 0, rating: 0, comment: '' };
  }

  setRating(star: number): void {
    this.feedback.rating = star;
  }

  ngOnInit(): void {
    this.resetForm();
  }

  submitFeedback(): void {
    if (this.isValid()) {
      console.log('Submitting feedback:', this.feedback);
      // Gửi dữ liệu feedback lên server (giả sử có một service xử lý)
      this.feedbackService.addFeedback(this.feedback).subscribe(response => {
        console.log('Feedback submitted successfully!', response);
        this.resetForm();
      });

      // Sau khi submit, reset form
      this.resetForm();
    }
  }

  resetForm(): void {
    this.feedback.rating = 0;
    this.feedback.comment = '';
  }

  isValid(): boolean {
    return this.feedback.rating > 0 && this.feedback.comment.trim().length > 0;
  }
}
