// src/app/features/chat/chat.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';
import { ChatService } from '../services/chat.service';
import { Message } from '../models/message.model';
import { User } from '../models/user.model';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  chatParticipants: number[] = [];
  chatParticipantsInfo: { [key: number]: User } = {};
  messages: Message[] = [];
  selectedUserId: number | null = null;
  content: string = '';
  currentUserId: number | undefined;
  errorMessage: string | null = null;

  constructor(
    public authService: AuthService,
    private chatService: ChatService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.currentUserId = this.authService.getUser()?.id;
    if (this.currentUserId) {
      // Kiểm tra query params để lấy otherUserId
      this.route.queryParams.subscribe(params => {
        const otherUserId = params['otherUserId'] ? Number(params['otherUserId']) : null;
        if (otherUserId && !isNaN(otherUserId)) {
          this.selectedUserId = otherUserId;
          // Thêm otherUserId vào danh sách chatParticipants nếu chưa có
          this.loadChatParticipants(otherUserId);
          this.loadChatHistory();
        } else {
          this.loadChatParticipants();
          this.loadUnreadMessages();
        }
      });
    } else {
      this.errorMessage = 'Vui lòng đăng nhập để sử dụng tính năng chat.';
    }
  }

  loadChatParticipants(otherUserId?: number): void {
    this.chatService.getChatParticipants().subscribe({
      next: (response) => {
        this.chatParticipants = response.data;
        // Nếu có otherUserId và nó chưa có trong danh sách, thêm vào
        if (otherUserId && !this.chatParticipants.includes(otherUserId)) {
          this.chatParticipants.push(otherUserId);
        }
        // Lấy thông tin của tất cả người trong danh sách
        this.chatParticipants.forEach(userId => {
          this.chatService.getUserInfo(userId).subscribe({
            next: (userResponse) => {
              this.chatParticipantsInfo[userId] = userResponse.data;
            },
            error: (err) => {
              console.error(`Error loading user info for ${userId}:`, err);
            }
          });
        });
        // Nếu không có otherUserId từ query params, chọn người đầu tiên trong danh sách
        if (!this.selectedUserId && this.chatParticipants.length > 0) {
          this.selectUser(this.chatParticipants[0]);
        }
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải danh sách người đã trò chuyện. Vui lòng thử lại sau.';
        console.error('Error loading chat participants:', err);
      }
    });
  }

  loadChatHistory(): void {
    if (this.currentUserId && this.selectedUserId) {
      this.chatService.getChatHistory(this.currentUserId, this.selectedUserId).subscribe({
        next: (response) => {
          this.messages = response.data;
          this.markUnreadMessagesAsRead();
        },
        error: (err) => {
          this.errorMessage = 'Không thể tải lịch sử tin nhắn. Vui lòng thử lại sau.';
          console.error('Error loading chat history:', err);
        }
      });
    }
  }

  markUnreadMessagesAsRead(): void {
    const unreadMessages = this.messages.filter(
      message => !message.isRead && message.receiverId === this.currentUserId
    );

    unreadMessages.forEach(message => {
      this.chatService.markMessageAsRead(message.id).subscribe({
        next: () => {
          message.isRead = true;
        },
        error: (err) => {
          console.error(`Error marking message ${message.id} as read:`, err);
        }
      });
    });
  }

  sendMessage(): void {
    if (this.currentUserId && this.selectedUserId && this.content.trim()) {
      this.chatService.sendMessage(this.currentUserId, this.selectedUserId, this.content).subscribe({
        next: () => {
          this.content = '';
          this.loadChatHistory();
          // Sau khi gửi tin nhắn, cập nhật lại danh sách chatParticipants
          this.loadChatParticipants(this.selectedUserId!);
        },
        error: (err) => {
          this.errorMessage = 'Không thể gửi tin nhắn. Vui lòng thử lại sau.';
          console.error('Error sending message:', err);
        }
      });
    } else {
      this.errorMessage = 'Vui lòng chọn người nhận và nhập nội dung tin nhắn.';
    }
  }

  loadUnreadMessages(): void {
    this.chatService.getUnreadMessages().subscribe({
      next: (response) => {
        console.log('Unread messages:', response.data);
      },
      error: (err) => {
        this.errorMessage = 'Không thể tải tin nhắn chưa đọc. Vui lòng thử lại sau.';
        console.error('Error loading unread messages:', err);
      }
    });
  }

  selectUser(userId: number): void {
    this.selectedUserId = userId;
    this.errorMessage = null;
    this.loadChatHistory();
  }
}