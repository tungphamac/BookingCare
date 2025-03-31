import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { AppointmentCreate } from '../../models/appointment-create.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update-appointment',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './update-appointment.component.html',
  styleUrls: ['./update-appointment.component.css']
})
export class UpdateAppointmentComponent implements OnInit {
  appointment: AppointmentCreate = {
    date: new Date(),
    time: '',
    reason: '',
    doctorId: 0,
    scheduleId: 0,
    clinicId: 0
  };
  schedules = [{ id: 1, time: '10:00 AM' }, { id: 2, time: '11:00 AM' }]; // Dữ liệu mẫu
  errorMessage: string = '';
  appointmentId: number = 0;

  constructor(
    private appointmentService: AppointmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.appointmentId = +this.route.snapshot.paramMap.get('id')!;
    // Tải thông tin lịch hẹn từ API nếu cần
  }

  onSubmit(): void {
    this.appointmentService.updateAppointment(this.appointmentId, this.appointment).subscribe({
      next: () => {
        alert('Cập nhật lịch hẹn thành công!');
        this.router.navigate(['/appointments']);
      },
      error: (error) => {
        this.errorMessage = error.error.message || 'Đã xảy ra lỗi khi cập nhật lịch hẹn.';
      }
    });
  }
}