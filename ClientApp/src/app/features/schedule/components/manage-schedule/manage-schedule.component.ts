import { Component, OnInit } from '@angular/core';
import { ScheduleService } from '../../services/schedule.service';
import { Schedule } from '../../models/schedule.model';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-schedule-management',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './manage-schedule.component.html',
  styleUrls: ['./manage-schedule.component.css']
})
export class ScheduleManagementComponent implements OnInit {
  schedules: Schedule[] = [];
  errorMessage: string | null = null;
  isLoading: boolean = false;
  doctorId: number | null = null;
  showEditPopup: boolean = false;
  showCreatePopup: boolean = false;
  selectedSchedule: Schedule & { startHour?: string; endHour?: string } | null = null;
  newSchedule: Schedule & { startHour?: string; endHour?: string } = { 
    id: 0, 
    doctorId: 0, 
    timeSlot: '', 
    startHour: '', 
    endHour: '', 
    workDate: new Date(), 
    status: 'Available' 
  };
  timeOptions: string[] = [];

  constructor(
    private scheduleService: ScheduleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // Tạo danh sách thời gian (cách nhau 30 phút)
    this.generateTimeOptions();
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(queryParams => {
      this.doctorId = queryParams['doctorId'] ? +queryParams['doctorId'] : null;

      if (this.doctorId) {
        this.loadSchedules();
      } else {
        this.errorMessage = 'Vui lòng cung cấp doctorId.';
      }
    });
  }

  // Tạo danh sách thời gian từ 00:00 đến 23:30
  generateTimeOptions(): void {
    for (let hour = 0; hour < 24; hour++) {
      for (let minute = 0; minute < 60; minute += 30) {
        const time = `${hour.toString().padStart(2, '0')}:${minute.toString().padStart(2, '0')}`;
        this.timeOptions.push(time);
      }
    }
  }

  loadSchedules(): void {
    if (!this.doctorId) {
      this.errorMessage = 'Không tìm thấy ID bác sĩ.';
      return;
    }

    this.isLoading = true;
    this.scheduleService.getSchedulesByDoctorId(this.doctorId).subscribe({
      next: (data) => {
        // Chuyển đổi timeSlot thành startHour và endHour để hiển thị trong modal chỉnh sửa
        this.schedules = data.map((schedule: Schedule) => {
          const [startHour, endHour] = schedule.timeSlot.split('-');
          return { ...schedule, startHour, endHour };
        });
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải danh sách lịch.';
        this.isLoading = false;
      }
    });
  }

  openCreatePopup(): void {
    this.newSchedule = { 
      id: 0, 
      doctorId: this.doctorId!, 
      timeSlot: '', 
      startHour: '', 
      endHour: '', 
      workDate: new Date(), 
      status: 'Available' 
    };
    this.showCreatePopup = true;
  }

  closeCreatePopup(): void {
    this.showCreatePopup = false;
  }

  createSchedule(): void {
    if (!this.doctorId) {
      this.errorMessage = 'Không tìm thấy ID bác sĩ.';
      return;
    }

    // Kiểm tra dữ liệu
    if (!this.newSchedule.startHour || !this.newSchedule.endHour) {
      this.errorMessage = 'Vui lòng chọn thời gian bắt đầu và kết thúc.';
      return;
    }

    // Chuyển đổi startHour và endHour thành timeSlot
    this.newSchedule.timeSlot = `${this.newSchedule.startHour}-${this.newSchedule.endHour}`;
    this.newSchedule.doctorId = this.doctorId;

    // Xóa startHour và endHour trước khi gửi lên API
    const { startHour, endHour, ...scheduleToSend } = this.newSchedule;

    this.scheduleService.createSchedule(scheduleToSend).subscribe({
      next: () => {
        this.showCreatePopup = false;
        this.loadSchedules();
        alert('Thêm lịch thành công.');
      },
      error: (err) => {
        this.errorMessage = 'Không thể thêm lịch.';
      }
    });
  }

  openEditPopup(schedule: Schedule): void {
    const [startHour, endHour] = schedule.timeSlot.split('-');
    this.selectedSchedule = { ...schedule, startHour, endHour };
    this.showEditPopup = true;
  }

  closeEditPopup(): void {
    this.showEditPopup = false;
    this.selectedSchedule = null;
  }

  updateSchedule(): void {
    if (!this.selectedSchedule) return;

    // Kiểm tra dữ liệu
    if (!this.selectedSchedule.startHour || !this.selectedSchedule.endHour) {
      this.errorMessage = 'Vui lòng chọn thời gian bắt đầu và kết thúc.';
      return;
    }

    // Chuyển đổi startHour và endHour thành timeSlot
    this.selectedSchedule.timeSlot = `${this.selectedSchedule.startHour}-${this.selectedSchedule.endHour}`;

    // Xóa startHour và endHour trước khi gửi lên API
    const { startHour, endHour, ...scheduleToSend } = this.selectedSchedule;

    this.scheduleService.updateSchedule(scheduleToSend.id, scheduleToSend).subscribe({
      next: () => {
        this.showEditPopup = false;
        this.loadSchedules();
        alert('Cập nhật lịch thành công.');
      },
      error: (err) => {
        this.errorMessage = 'Không thể cập nhật lịch.';
      }
    });
  }

  deleteSchedule(id: number): void {
    if (confirm('Bạn có chắc chắn muốn xóa lịch này?')) {
      this.scheduleService.deleteSchedule(id).subscribe({
        next: () => {
          this.schedules = this.schedules.filter(schedule => schedule.id !== id);
          alert('Xóa lịch thành công.');
        },
        error: (err) => {
          this.errorMessage = 'Không thể xóa lịch.';
        }
      });
    }
  }
}