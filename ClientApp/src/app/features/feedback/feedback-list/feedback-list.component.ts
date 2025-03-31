import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Feedback } from '../models/feedback';
import { FeedbackService } from '../services/feedback.service';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-feedback-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.css']
})
export class FeedbackListComponent implements OnInit {
  feedbackList: Feedback[] = [];
  filteredFeedback: Feedback[] = [];
  ratingFilter: string = 'all';
  currentPage: number = 1;
  itemsPerPage: number = 10;

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
    this.loadFeedback();
  }

  loadFeedback(): void {
    this.feedbackService.getFeedbacks().subscribe(
      response => {

        if (!response || !response.data || !Array.isArray(response.data)) {
          console.error('API returned invalid data format:', response);
          this.feedbackList = [];
        } else {
          this.feedbackList = response.data; // Chỉ lấy mảng feedbacks từ API
        }

        this.applyFilters();
      },
      error => {
        console.error('Error loading feedback:', error);
        this.feedbackList = [];
        this.applyFilters();
      }
    );
  }

  applyFilters(): void {
    console.log('Before Filtering:', this.feedbackList);

    if (!Array.isArray(this.feedbackList)) {
      this.feedbackList = [];
    }
    this.filteredFeedback = this.ratingFilter === 'all'
      ? [...this.feedbackList]
      : this.feedbackList.filter(fb => fb.rating.toString() === this.ratingFilter);
    console.log('After Filtering:', this.filteredFeedback);
    this.currentPage = 1;
  }

  deleteFeedback(feedbackId: number): void {
    if (confirm('Bạn có chắc muốn xoá feedback này không?')) {
      this.feedbackService.deleteFeedback(feedbackId).subscribe(() => {
        this.feedbackList = this.feedbackList.filter(fb => fb.id !== feedbackId);
        this.applyFilters();
      });
    }
  }

  calculateAverageRating(): number {
    if (!this.feedbackList.length) return 0;
    const totalRating = this.feedbackList.reduce((sum, feedback) => sum + (feedback.rating || 0), 0);
    return Math.round((totalRating / this.feedbackList.length) * 10) / 10;
  }

  get paginatedFeedback(): Feedback[] {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    return this.filteredFeedback.slice(start, start + this.itemsPerPage);
  }

  getTotalPages(): number {
    return Math.ceil(this.filteredFeedback.length / this.itemsPerPage);
  }

  getPageNumbers(): number[] {
    return Array.from({ length: this.getTotalPages() }, (_, i) => i + 1);
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.getTotalPages()) {
      this.currentPage = page;
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  nextPage(): void {
    if (this.currentPage < this.getTotalPages()) {
      this.currentPage++;
    }
  }

  getPaginatedFeedback(): Feedback[] {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.filteredFeedback.slice(start, end);
  }
}
