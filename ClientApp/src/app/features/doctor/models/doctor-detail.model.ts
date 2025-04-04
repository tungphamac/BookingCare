// src\app\features\doctor\models\doctor-detail.model.ts
export interface DoctorDetailDto {
    id: number;
    userName: string;
    email: string;
    gender: boolean;
    address: string;
    avatar: string;
    achievement: string;
    description: string;
    specializationName: string;
    clinicName: string;
  }