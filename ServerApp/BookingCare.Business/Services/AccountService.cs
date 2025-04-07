using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace BookingCare.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly string _frontendUrl;

        public AccountService(UserManager<User> userManager, IEmailService emailService, ILogger<AccountService> logger,IConfiguration configuration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _frontendUrl = configuration["FrontendUrl"];
        }
        public async Task<(bool Success, string Message, string[] Errors)> ChangePasswordAsync(int userId, string oldPassword, string newPassword, string confirmNewPassword)
        {

            if (newPassword != confirmNewPassword)
                return (false, "Mật khẩu mới và xác nhận mật khẩu không khớp.", null);


            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return (false, "Không tìm thấy người dùng.", null);
            //var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            //if (user == null)
            //    return (false, "Không tìm thấy người dùng.", null);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();
                return (false, "Đổi mật khẩu thất bại.", errors);
            }

            return (true, "Đổi mật khẩu thành công.", null);
        }

        public async Task<(bool Success, string Message)> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, "Email không tồn tại.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{_frontendUrl}/reset-password?email={email}&token={Uri.EscapeDataString(token)}";

            try
            {
                var emailBody = $"<p>Xin chào,</p>" +
                                $"<p>Chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản <strong>{email}</strong>.</p>" +
                                $"<p>Nhấp vào liên kết sau để đặt lại mật khẩu: <a href='{resetLink}'>{resetLink}</a></p>" +
                                $"<p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>" +
                                $"<p>Trân trọng,<br>BookingCare Team</p>";

                await _emailService.SendEmailAsync(
                    email, // Email của khách hàng làm địa chỉ nhận (To)
                    "Đặt lại mật khẩu",
                    emailBody
                );
            }
            catch (Exception ex)
            {
                return (false, $"Không thể gửi email: {ex.Message}");
            }

            return (true, "Email đặt lại mật khẩu đã được gửi.");
        }

        public async Task<(bool Success, string Message, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
                return (false, "Mật khẩu mới và xác nhận mật khẩu không khớp.", null);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, "Email không tồn tại.", null);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();
                return (false, "Đặt lại mật khẩu thất bại.", errors);
            }

            return (true, "Mật khẩu đã được đặt lại thành công.", null);
        }

        public async Task<(bool Success, string Message, DateTime? LockUntil)> LockUserAccountAsync(int userId, DateTime lockUntil)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return (false, "User not found.", null);
            }

            user.LockoutEnabled = true;
            user.LockoutEnd = lockUntil;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return (true, $"User with Id {userId} has been locked until {lockUntil}.", lockUntil);
            }

            return (false, "Failed to lock user account.", null);
        }



        public async Task<UserDetailsVm> GetUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return null;
            }

            // Chuyển thông tin người dùng thành UserDetailsVm
            var userDetailsVm = new UserDetailsVm
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                LockoutEnd = user.LockoutEnd?.DateTime ?? DateTime.MinValue, // Chuyển từ DateTimeOffset? sang DateTime?
                LockoutEnabled = user.LockoutEnabled
            };

            return userDetailsVm;
        }


        public async Task<bool> UnlockUserAccountAsync(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    _logger.LogWarning($"User with Id {userId} not found.");
                    return false;
                }

                // Đặt LockoutEnabled thành false và LockoutEnd là null để mở khóa tài khoản
                user.LockoutEnabled = false;
                user.LockoutEnd = null;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User with Id {userId} unlocked.");
                    return true;
                }

                _logger.LogError($"Failed to unlock account for user {userId}.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error unlocking account for user {userId}.");
                throw;
            }
        }

        public async Task<List<UserDetailsVm>> GetAllUsersAsync()
        {
            // Lấy tất cả người dùng từ cơ sở dữ liệu
            var users = await _userManager.Users.ToListAsync();

            var userDetailsList = users.Select(user => new UserDetailsVm
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                LockoutEnd = user.LockoutEnd?.DateTime ?? DateTime.MinValue, // Chuyển từ DateTimeOffset? sang DateTime?
                LockoutEnabled = user.LockoutEnabled
            }).ToList();

            return userDetailsList;
        }

        // Các phương thức khác: ChangePasswordAsync, ForgotPasswordAsync, ResetPasswordAsync
    }

}

