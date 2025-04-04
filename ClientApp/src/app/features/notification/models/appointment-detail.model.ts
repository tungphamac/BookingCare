export interface AppointmentDetailDto {
    id: number;
    doctorName: string;
    patientName: string;
    scheduleTime: string; // DateTime từ backend sẽ được chuyển thành string trong JSON
    status: string;
    createdAt: string;
  }