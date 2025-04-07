using BookingCare.Business.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IChatService
    {
        Task<int> SendMessageAsync(SendMessageDto dto);                    // Gửi tin nhắn
        Task<List<MessageDetailDto>> GetChatHistoryAsync(int userId1, int userId2); // Lấy lịch sử chat giữa 2 người
        Task<bool> MarkMessageAsReadAsync(int messageId, int userId);      // Đánh dấu tin nhắn là đã đọc
        Task<List<MessageDetailDto>> GetUnreadMessagesAsync(int userId);   // Lấy danh sách tin nhắn chưa đọc
        Task<List<int>> GetChatParticipantsAsync(int userId);
        Task<UserInfoDto> GetUserInfoAsync(int userId);
    }
}
