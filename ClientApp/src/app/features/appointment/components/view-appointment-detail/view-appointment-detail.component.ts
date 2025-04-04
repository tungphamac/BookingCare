import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentDetail } from '../../models/appointment-detail.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view-appointment-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-appointment-detail.component.html',
  styleUrls: ['./view-appointment-detail.component.css']
})
export class ViewAppointmentDetailComponent implements OnInit {
  appointment: AppointmentDetail | null = null;

  constructor(
    private appointmentService: AppointmentService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.appointmentService.getAppointmentDetail(id).subscribe({
      next: (response) => {
        this.appointment = response.data;
      },
      error: (error) => {
        console.error('Lỗi khi xem chi tiết lịch hẹn:', error);
      }
    });
  }
}