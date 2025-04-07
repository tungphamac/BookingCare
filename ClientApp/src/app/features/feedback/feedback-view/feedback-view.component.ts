import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Feedback } from '../models/feedback';
import { FeedbackService } from '../services/feedback.service';

@Component({
  selector: 'app-feedback-view',
  standalone: true, // Thêm standalone: true
  imports: [CommonModule],
  templateUrl: './feedback-view.component.html',
  styleUrl: './feedback-view.component.css'
})
export class FeedbackViewComponent implements OnInit, OnChanges {
  feedbackList: Feedback[] = [];
  @Input() doctorId!: number;
  
  constructor(private feedbackService: FeedbackService) { }
  
  ngOnInit(): void {
    this.loadFeedbacks();
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    // Xử lý khi doctorId thay đổi
    if (changes['doctorId'] && changes['doctorId'].currentValue) {
      this.loadFeedbacks();
    }
  }
  
  loadFeedbacks(): void {
    if (!this.doctorId) {
      console.error('Doctor ID is undefined!');
      return;
    }
    
    console.log('Loading feedbacks for doctor ID:', this.doctorId);
    this.feedbackService.getFeedbacksByDoctor(this.doctorId).subscribe({
      next: response => {
        this.feedbackList = response;
        console.log("Feedback list loaded:", this.feedbackList);
      },
      error: err => {
        console.error('Error fetching feedbacks:', err);
      }
    });
  }
}