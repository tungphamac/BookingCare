// src\app\features\clinic\models\clinic-detail.model.ts
export interface ClinicDetailDto {
    id: number;
    name: string;
    address: string;
    phone: number;
    introduction: string;
    createAt: Date; // DateTime từ backend
  }