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
  scheduleId: number | null = null;
  mode: 'list' | 'view' | 'create' | 'update' = 'list'; // Chế độ hiển thị
  selectedSchedule: Schedule | null = null;
  newSchedule: Schedule = { id: 0, doctorId: 0, timeSlot: '', workDate: new Date(), status: 'Available' };

  constructor(
    private scheduleService: ScheduleService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  cancelUpdate(): void {
    this.router.navigate(['/schedules'], { queryParams: { doctorId: this.doctorId } });
  }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.scheduleId = params['id'] ? +params['id'] : null;
    });
    

    this.route.queryParams.subscribe(queryParams => {
      this.doctorId = queryParams['doctorId'] ? +queryParams['doctorId'] : null;

      // Xác định mode dựa trên route
      if (this.router.url.startsWith('/schedules/create')) {
        this.mode = 'create';
      } else if (this.router.url.startsWith('/schedules/update') && this.scheduleId) {
        this.mode = 'update';
        this.loadScheduleForEdit(this.scheduleId);
      } else if (this.scheduleId) {
        this.mode = 'view';
        this.getScheduleById(this.scheduleId);
      } else if (this.doctorId) {
        this.mode = 'list';
        this.loadSchedules();
      } else {
        this.errorMessage = 'Vui lòng cung cấp doctorId hoặc scheduleId.';
      }
    });
  }

  loadSchedules(): void {
    if (!this.doctorId) {
      this.errorMessage = 'Không tìm thấy ID bác sĩ.';
      return;
    }

    this.isLoading = true;
    this.scheduleService.getSchedulesByDoctorId(this.doctorId).subscribe({
      next: (data) => {
        this.schedules = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải danh sách lịch.';
        this.isLoading = false;
      }
    });
  }

  getScheduleById(id: number): void {
    this.isLoading = true;
    this.scheduleService.getScheduleById(id).subscribe({
      next: (data) => {
        this.schedules = [data];
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải chi tiết lịch.';
        this.isLoading = false;
      }
    });
  }

  loadScheduleForEdit(id: number): void {
    this.scheduleService.getScheduleById(id).subscribe({
      next: (data) => {
        this.selectedSchedule = { ...data };
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải lịch để chỉnh sửa.';
      }
    });
  }

  createSchedule(): void {
    if (!this.doctorId) {
      this.errorMessage = 'Không tìm thấy ID bác sĩ.';
      return;
    }

    this.newSchedule.doctorId = this.doctorId;
    this.scheduleService.createSchedule(this.newSchedule).subscribe({
      next: () => {
        this.router.navigate(['/schedules'], { queryParams: { doctorId: this.doctorId } });
        alert('Thêm lịch thành công.');
      },
      error: (err) => {
        this.errorMessage = 'Không thể thêm lịch.';
      }
    });
  }

  updateSchedule(): void {
    if (!this.selectedSchedule) return;

    this.scheduleService.updateSchedule(this.selectedSchedule.id, this.selectedSchedule).subscribe({
      next: () => {
        this.router.navigate(['/schedules'], { queryParams: { doctorId: this.doctorId } });
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