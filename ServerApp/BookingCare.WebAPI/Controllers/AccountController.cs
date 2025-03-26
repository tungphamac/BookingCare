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
            Console.WriteLine("Request reached change-password endpoint");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"UserId Claim: {userIdClaim}");

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
    }

}
