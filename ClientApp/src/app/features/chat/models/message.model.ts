// src/app/models/message.model.ts
export interface Message {
    id: number;
    senderId: number;
    receiverId: number;
    content: string;
    sentAt: Date;
    isRead: boolean;
  }