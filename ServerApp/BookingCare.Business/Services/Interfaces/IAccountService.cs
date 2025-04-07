using BookingCare.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<(bool Success, string Message, string[] Errors)> ChangePasswordAsync(int userId, string oldPassword, string newPassword, string confirmNewPassword);
        Task<(bool Success, string Message)> ForgotPasswordAsync(string email);
        Task<(bool Success, string Message, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword, string confirmNewPassword);
        Task<UserDetailsVm> GetUserByIdAsync(int userId);  // Thêm phương thức GetById
        Task<(bool Success, string Message, DateTime? LockUntil)> LockUserAccountAsync(int userId, DateTime lockUntil);

        Task<bool> UnlockUserAccountAsync(int userId);
        Task<List<UserDetailsVm>> GetAllUsersAsync();
    }
}
