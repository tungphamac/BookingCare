using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Data;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

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
            var resetLink = $"https://yourdomain.com/reset-password?email={email}&token={Uri.EscapeDataString(token)}";

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
    }
}
