export interface Notification {
    id: number;
    message: string;
    appointmentId: number;
    isRead: boolean;
    createdAt: string;
  }