// src/app/features/notification/models/appointment-detail.model.ts
export interface AppointmentDetailDto {
  id: number;
  doctorName: string;
  patientName: string;
  scheduleTime: string; // DateTime từ backend sẽ được chuyển thành string trong JSON
  reason: string;
  clinicId: number;
  status: string;
  createdAt: string; // DateTime từ backend sẽ được chuyển thành string trong JSON
}