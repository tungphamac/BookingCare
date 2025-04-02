export interface MedicalRecordDetail {
    id: number;
    appointmentId: number;
    diagnosis: string;
    prescription: string;
    notes: string;
    createdBy: number;
    createdAt: Date;
  }