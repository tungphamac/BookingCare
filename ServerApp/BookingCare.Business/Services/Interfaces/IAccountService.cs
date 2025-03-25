using BookingCare.Business.Services.Base;
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
    }
}
