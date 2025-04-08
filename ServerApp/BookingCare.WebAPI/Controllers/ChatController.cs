using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        /// <summary>
        /// Gửi một tin nhắn từ người dùng hiện tại đến người nhận.
        /// </summary>
        /// <param name="dto">Dữ liệu tin nhắn bao gồm SenderId, ReceiverId và nội dung.</param>
        /// <returns>Trả về thông báo thành công hoặc lỗi nếu có vấn đề xảy ra.</returns>
        [HttpPost("send")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            try
            {
                var senderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (senderId != dto.SenderId)
                {
                    _logger.LogWarning("SenderId từ token ({SenderId}) không khớp với yêu cầu ({DtoSenderId}).", senderId, dto.SenderId);
                    return Unauthorized(new { Success = false, Message = "Bạn không có quyền gửi tin nhắn từ tài khoản này." });
                }

                var messageId = await _chatService.SendMessageAsync(dto);
                _logger.LogInformation("Tin nhắn được gửi từ User {SenderId} đến User {ReceiverId}.", dto.SenderId, dto.ReceiverId);
                return Ok(new { Success = true, Message = "Tin nhắn đã được gửi thành công.", Data = messageId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi tin nhắn từ User {SenderId} đến User {ReceiverId}.", dto.SenderId, dto.ReceiverId);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi gửi tin nhắn." });
            }
        }

        /// <summary>
        /// Lấy lịch sử trò chuyện giữa hai người dùng.
        /// </summary>
        /// <param name="userId1">ID của người dùng đầu tiên.</param>
        /// <param name="userId2">ID của người dùng thứ hai.</param>
        /// <returns>Trả về danh sách tin nhắn hoặc thông báo lỗi nếu không có quyền.</returns>
        [HttpGet("history")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> GetChatHistory([FromQuery] int userId1, [FromQuery] int userId2)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (currentUserId != userId1 && currentUserId != userId2)
                {
                    _logger.LogWarning("User {CurrentUserId} không được phép xem lịch sử trò chuyện giữa {UserId1} và {UserId2}.", currentUserId, userId1, userId2);
                    return Unauthorized(new { Success = false, Message = "Bạn không có quyền xem lịch sử tin nhắn này." });
                }

                var messages = await _chatService.GetChatHistoryAsync(userId1, userId2);
                _logger.LogInformation("Lấy lịch sử trò chuyện giữa User {UserId1} và User {UserId2} thành công.", userId1, userId2);
                return Ok(new { Success = true, Message = "Lấy lịch sử tin nhắn thành công.", Data = messages });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy lịch sử trò chuyện giữa User {UserId1} và User {UserId2}.", userId1, userId2);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi lấy lịch sử tin nhắn." });
            }
        }

        /// <summary>
        /// Đánh dấu một tin nhắn là đã đọc.
        /// </summary>
        /// <param name="messageId">ID của tin nhắn cần đánh dấu.</param>
        /// <returns>Trả về thông báo thành công hoặc lỗi nếu tin nhắn không tồn tại.</returns>
        [HttpPut("read")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> MarkMessageAsRead([FromQuery] int messageId)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await _chatService.MarkMessageAsReadAsync(messageId, currentUserId);
                if (!result)
                {
                    _logger.LogWarning("Tin nhắn {MessageId} không tồn tại hoặc User {UserId} không phải người nhận.", messageId, currentUserId);
                    return NotFound(new { Success = false, Message = "Tin nhắn không tồn tại hoặc bạn không phải người nhận." });
                }

                _logger.LogInformation("Tin nhắn {MessageId} được đánh dấu là đã đọc bởi User {UserId}.", messageId, currentUserId);
                return Ok(new { Success = true, Message = "Tin nhắn đã được đánh dấu là đã đọc." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đánh dấu tin nhắn {MessageId} là đã đọc bởi User {UserId}.", messageId, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi đánh dấu tin nhắn là đã đọc." });
            }
        }

        /// <summary>
        /// Lấy danh sách tin nhắn chưa đọc của người dùng hiện tại.
        /// </summary>
        /// <param name="userId">ID của người dùng cần lấy tin nhắn chưa đọc.</param>
        /// <returns>Trả về danh sách tin nhắn chưa đọc hoặc thông báo lỗi nếu không có quyền.</returns>
        [HttpGet("unread")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> GetUnreadMessages([FromQuery] int userId)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (currentUserId != userId)
                {
                    _logger.LogWarning("User {CurrentUserId} không được phép xem tin nhắn chưa đọc của User {UserId}.", currentUserId, userId);
                    return Unauthorized(new { Success = false, Message = "Bạn không có quyền xem tin nhắn chưa đọc của người dùng này." });
                }

                var unreadMessages = await _chatService.GetUnreadMessagesAsync(userId);
                _logger.LogInformation("Lấy {Count} tin nhắn chưa đọc cho User {UserId}.", unreadMessages.Count, userId);
                return Ok(new { Success = true, Message = "Lấy danh sách tin nhắn chưa đọc thành công.", Data = unreadMessages });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách tin nhắn chưa đọc cho User {UserId}.", userId);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi lấy danh sách tin nhắn chưa đọc." });
            }
        }
        [HttpGet("participants")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> GetChatParticipants()
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var participants = await _chatService.GetChatParticipantsAsync(currentUserId);
                _logger.LogInformation("Lấy danh sách {Count} người đã trò chuyện với User {UserId}.", participants.Count, currentUserId);
                return Ok(new { Success = true, Message = "Lấy danh sách người đã trò chuyện thành công.", Data = participants });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách người đã trò chuyện với User {UserId}.", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi lấy danh sách người đã trò chuyện." });
            }
        }
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            try
            {
                var userInfo = await _chatService.GetUserInfoAsync(userId);
                _logger.LogInformation("Lấy thông tin người dùng {UserId} thành công.", userId);
                return Ok(new { Success = true, Message = "Lấy thông tin người dùng thành công.", Data = userInfo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin người dùng {UserId}.", userId);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi lấy thông tin người dùng." });
            }
        }
    }
}