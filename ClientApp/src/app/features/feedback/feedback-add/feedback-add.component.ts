import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Feedback } from '../models/feedback';
import { FeedbackService } from '../services/feedback.service';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-feedback-add',
  imports: [CommonModule, FormsModule],
  templateUrl: './feedback-add.component.html',
  styleUrl: './feedback-add.component.css'
})
export class FeedbackAddComponent implements OnInit {
  feedback: Feedback;
  appointmentId: number = 0;
  hasFeedback: boolean = false; // Biến để kiểm tra xem đã có feedback chưa
  errorMessage: string = ''; // Thông báo lỗi nếu có
  successMessage: string = ''; // Thông báo thành công nếu có

  constructor(
    private feedbackService: FeedbackService,
    private route: ActivatedRoute,
    private router: Router // Thêm Router để điều hướng
  ) {
    this.feedback = { id: 0, patientName: '', appointmentId: 0, rating: 0, comment: '', createAt: new Date() };
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.appointmentId = +params['appointmentId'];
      this.feedback.appointmentId = this.appointmentId;

      // Giả lập patientName (thay bằng logic lấy từ người dùng đăng nhập)
      this.feedback.patientName = 'patient1@example.com';

      // Kiểm tra xem cuộc hẹn đã có feedback chưa
      this.checkExistingFeedback();
    });
  }

  checkExistingFeedback(): void {
    this.feedbackService.getFeedbackByAppointment(this.appointmentId).subscribe({
      next: (existingFeedback) => {
        if (existingFeedback) {
          this.hasFeedback = true;
          this.errorMessage = 'This appointment already has a feedback. You cannot submit another one.';
        } else {
          this.hasFeedback = false;
          this.resetForm();
        }
      },
      error: (error) => {
        if (error.status === 404) {
          // Không tìm thấy feedback, nghĩa là chưa có feedback
          this.hasFeedback = false;
          this.resetForm();
        } else {
          console.error('Error checking existing feedback:', error);
          this.errorMessage = 'An error occurred while checking feedback status.';
        }
      }
    });
  }

  setRating(star: number): void {
    if (!this.hasFeedback) {
      this.feedback.rating = star;
    }
  }

  submitFeedback(): void {
    if (this.isValid() && !this.hasFeedback) {
      this.feedback.createAt = new Date();
      console.log('Submitting feedback:', this.feedback);

      this.feedbackService.addFeedback(this.feedback).subscribe({
        next: (response) => {
          this.successMessage = response.message || 'Feedback submitted successfully!';
          this.hasFeedback = true; // Đánh dấu đã có feedback
          setTimeout(() => {
            this.router.navigate(['/feedbacks/view', this.appointmentId]);
          }, 2000); // Chuyển hướng sau 2 giây để người dùng thấy thông báo
        },
        error: (error) => {
          console.error('Error submitting feedback:', error);
          if (error.status === 400 && error.error.message) {
            this.errorMessage = error.error.message;
            this.hasFeedback = true; // Đánh dấu đã có feedback nếu backend báo lỗi
          } else {
            this.errorMessage = 'An error occurred while submitting feedback.';
          }
        }
      });
    }
  }

  resetForm(): void {
    this.feedback = {
      id: 0,
      patientName: this.feedback.patientName,
      appointmentId: this.appointmentId,
      rating: 0,
      comment: '',
      createAt: new Date()
    };
    this.errorMessage = '';
    this.successMessage = '';
  }

  isValid(): boolean {
    return this.feedback.rating > 0 && this.feedback.comment.trim().length > 0;
  }
}
