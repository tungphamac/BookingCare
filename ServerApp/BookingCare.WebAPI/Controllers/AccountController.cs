using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Controller is working!");
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVm request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Console.WriteLine($"UserId Claim: {userIdClaim}"); // Sẽ in "13"

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))

                return Unauthorized(new { message = "Không thể xác định người dùng." });

            var (success, message, errors) = await _accountService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword, request.ConfirmNewPassword);
            if (!success)
                return BadRequest(new { message, errors });

            return Ok(new { message });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordVm request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, message) = await _accountService.ForgotPasswordAsync(request.Email);
            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordVm request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, message, errors) = await _accountService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword, request.ConfirmNewPassword);
            if (!success)
                return BadRequest(new { message, errors });

            return Ok(new { message });
        }

        [HttpPost("lock/{userId}")]
        public async Task<IActionResult> LockUserAccount(int userId, [FromBody] string duration)
        {
            DateTime lockUntil = DateTime.UtcNow;

            if (duration == "24h")
            {
                lockUntil = DateTime.UtcNow.AddHours(24);
            }
            else if (duration == "7d")
            {
                lockUntil = DateTime.UtcNow.AddDays(7);
            }
            else
            {
                return BadRequest(new { Message = "Invalid duration." });
            }

            var (success, message, lockUntilTime) = await _accountService.LockUserAccountAsync(userId, lockUntil);

            if (!success)
            {
                return NotFound(new { Message = message });
            }


            return Ok(new { Message = message, LockUntil = lockUntil.ToString("yyyy-MM-dd HH:mm:ss") });
        }


        // Phương thức API mở khóa tài khoản
        [HttpPost("unlock/{userId}")]
        public async Task<IActionResult> UnlockUserAccount(int userId)
        {
            var result = await _accountService.UnlockUserAccountAsync(userId);

            if (!result)
            {
                return NotFound(new { Message = $"User with Id {userId} not found." });
            }

            return Ok(new { Message = $"User with Id {userId} has been unlocked." });
        }
        [HttpGet("getby/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _accountService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = $"User with Id {userId} not found." });
            }

            return Ok(user);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _accountService.GetAllUsersAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound(new { Message = "No users found." });
            }

            return Ok(users);
        }
    }

}
