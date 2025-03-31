export interface AppointmentCreate {
    date: Date;
    time: string;
    reason: string;
    doctorId: number;
    scheduleId: number;
    clinicId: number;
  }