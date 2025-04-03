export interface NotificationDto {
    id: number;
    message: string;
    appointmentId: number;
    isRead: boolean;
    createdAt: string; // DateTime từ backend sẽ được chuyển thành string trong JSON
  }