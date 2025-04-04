// src\app\features\clinic\models\clinic.ts
export interface Clinic {
  id: number; // Thêm trường id vào đây
  name: string;
  address: string;
  phone: number;
  introduction: string;
  createAt: string; // DateTime từ backend sẽ được xử lý dưới dạng string
}