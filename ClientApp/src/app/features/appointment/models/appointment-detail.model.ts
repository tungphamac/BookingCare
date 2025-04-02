export interface AppointmentDetail {
  id: number;
  doctorName: string;
  patientName: string;
  scheduleTime: string;
  status: string;
  createdAt?: string;
  reason?: string;
  clinicId?: number;
}