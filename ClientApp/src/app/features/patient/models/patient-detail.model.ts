export interface PatientDetail {
  id: number;
  userName: string;
  email: string;
  gender: boolean;
  phone: number;
  address: string;
  avatar: File;
  medicalRecordId: number;
}