using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using BookingCare.Business.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingCare.Business.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ChatService> _logger;

        public ChatService(IUnitOfWork unitOfWork, ILogger<ChatService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // Gửi tin nhắn
        public async Task<int> SendMessageAsync(SendMessageDto dto)
        {
            try
            {
                // Kiểm tra xem người nhận có tồn tại không
                var receiverExists = await _unitOfWork.UserRepository
                    .GetQuery(u => u.Id == dto.ReceiverId)
                    .AnyAsync();

                if (!receiverExists)
                {
                    _logger.LogWarning("Người nhận với ID {ReceiverId} không tồn tại.", dto.ReceiverId);
                    throw new Exception("Người nhận không tồn tại.");
                }

                var message = new Message
                {
                    SenderId = dto.SenderId,
                    ReceiverId = dto.ReceiverId,
                    Content = dto.Content,
                    SentAt = DateTime.UtcNow,
                    IsRead = false
                };

                await _unitOfWork.MessageRepository.AddAsync(message);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Tin nhắn gửi từ User {SenderId} đến User {ReceiverId}.", dto.SenderId, dto.ReceiverId);
                return message.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi tin nhắn từ User {SenderId} đến User {ReceiverId}.", dto.SenderId, dto.ReceiverId);
                throw;
            }
        }

        // Lấy lịch sử chat giữa 2 người
        public async Task<List<MessageDetailDto>> GetChatHistoryAsync(int userId1, int userId2)
        {
            try
            {
                var messages = await _unitOfWork.MessageRepository
                    .GetQuery(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1))
                    .OrderBy(m => m.SentAt)
                    .Select(m => new MessageDetailDto
                    {
                        Id = m.Id,
                        SenderId = m.SenderId,
                        ReceiverId = m.ReceiverId,
                        Content = m.Content,
                        SentAt = m.SentAt,
                        IsRead = m.IsRead
                    })
                    .ToListAsync();

                _logger.LogInformation("Lấy lịch sử chat giữa User {UserId1} và User {UserId2}.", userId1, userId2);
                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy lịch sử chat giữa User {UserId1} và User {UserId2}.", userId1, userId2);
                throw;
            }
        }

        // Đánh dấu tin nhắn là đã đọc
        public async Task<bool> MarkMessageAsReadAsync(int messageId, int userId)
        {
            try
            {
                var message = await _unitOfWork.MessageRepository.GetByIdAsync(messageId);
                if (message == null)
                {
                    _logger.LogWarning("Không tìm thấy tin nhắn với ID {MessageId}.", messageId);
                    return false;
                }

                if (message.ReceiverId != userId)
                {
                    _logger.LogWarning("User {UserId} không phải người nhận của tin nhắn {MessageId}.", userId, messageId);
                    return false;
                }

                message.IsRead = true;
                _unitOfWork.MessageRepository.Update(message);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Tin nhắn {MessageId} được đánh dấu đã đọc bởi User {UserId}.", messageId, userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đánh dấu tin nhắn {MessageId} đã đọc bởi User {UserId}.", messageId, userId);
                throw;
            }
        }

        // Lấy danh sách tin nhắn chưa đọc
        public async Task<List<MessageDetailDto>> GetUnreadMessagesAsync(int userId)
        {
            try
            {
                var unreadMessages = await _unitOfWork.MessageRepository
                    .GetQuery(m => m.ReceiverId == userId && !m.IsRead)
                    .OrderBy(m => m.SentAt)
                    .Select(m => new MessageDetailDto
                    {
                        Id = m.Id,
                        SenderId = m.SenderId,
                        ReceiverId = m.ReceiverId,
                        Content = m.Content,
                        SentAt = m.SentAt,
                        IsRead = m.IsRead
                    })
                    .ToListAsync();

                _logger.LogInformation("Lấy {Count} tin nhắn chưa đọc cho User {UserId}.", unreadMessages.Count, userId);
                return unreadMessages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy tin nhắn chưa đọc cho User {UserId}.", userId);
                throw;
            }
        }

        // Lấy danh sách người đã trò chuyện
        public async Task<List<int>> GetChatParticipantsAsync(int userId)
        {
            try
            {
                var participants = await _unitOfWork.MessageRepository
                    .GetQuery(m => m.SenderId == userId || m.ReceiverId == userId)
                    .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                    .Distinct()
                    .ToListAsync();

                _logger.LogInformation("Lấy danh sách {Count} người đã trò chuyện với User {UserId}.", participants.Count, userId);
                return participants;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách người đã trò chuyện với User {UserId}.", userId);
                throw;
            }
        }

        // Lấy thông tin người dùng
        public async Task<UserInfoDto> GetUserInfoAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository
                    .GetQuery(u => u.Id == userId)
                    .Select(u => new UserInfoDto
                    {
                        Id = u.Id,
                        Username = u.UserName,
                        Email = u.Email
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    _logger.LogWarning("Không tìm thấy người dùng với ID {UserId}.", userId);
                    throw new Exception("Người dùng không tồn tại.");
                }

                _logger.LogInformation("Lấy thông tin người dùng {UserId} thành công.", userId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin người dùng {UserId}.", userId);
                throw;
            }
        }
    }
}