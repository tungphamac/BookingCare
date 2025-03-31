export interface AppointmentDetail {
    id: number;
    doctorName: string;
    patientName: string;
    scheduleTime: Date;
    status: string;
    createdAt: Date;
  }