using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using BookingCare.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace BookingCareTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IAccountService> _mockAccountService;
        private AccountController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();

            _controller = new AccountController(_mockAccountService.Object);

            // Giả lập ClaimsPrincipal với UserId là "13"
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, "13")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Test]
        public async Task ChangePassword_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new ChangePasswordVm
            {
                OldPassword = "oldpass",
                NewPassword = "newpass123",
                ConfirmNewPassword = "newpass123"
            };

            _mockAccountService.Setup(s => s.ChangePasswordAsync(
                13, request.OldPassword, request.NewPassword, request.ConfirmNewPassword))
                .ReturnsAsync((true, "Password changed successfully", null));

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.Not.Null);
            Assert.That(okResult.Value.ToString(), Does.Contain("Password changed successfully"));
        }
        [Test]
        public async Task ChangePassword_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("OldPassword", "Required");
            var request = new ChangePasswordVm();

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task ChangePassword_UnauthorizedUser_ReturnsUnauthorized()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var request = new ChangePasswordVm
            {
                OldPassword = "oldpass",
                NewPassword = "newpass",
                ConfirmNewPassword = "newpass"
            };

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
        }

        [Test]
        public async Task ChangePassword_ServiceFails_ReturnsBadRequest()
        {
            // Arrange
            var request = new ChangePasswordVm
            {
                OldPassword = "wrongOld",
                NewPassword = "newpass",
                ConfirmNewPassword = "newpass"
            };

            _mockAccountService.Setup(s => s.ChangePasswordAsync(
                13, request.OldPassword, request.NewPassword, request.ConfirmNewPassword))
                .ReturnsAsync((false, "Old password is incorrect", new[] { "Old password doesn't match" }));

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badResult = result as BadRequestObjectResult;
            Assert.That(badResult.Value.ToString(), Does.Contain("Old password is incorrect"));
        }

        [Test]
        public async Task ForgotPassword_FailedServiceCall_ReturnsBadRequest()
        {
            // Arrange
            var request = new ForgotPasswordVm { Email = "invalid@example.com" };

            _mockAccountService.Setup(s => s.ForgotPasswordAsync(request.Email))
                .ReturnsAsync((false, "Email không tồn tại"));

            // Act
            var result = await _controller.ForgotPassword(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badResult = result as BadRequestObjectResult;
            Assert.That(badResult.Value.ToString(), Does.Contain("Email không tồn tại"));
        }

        [Test]
        public async Task ForgotPassword_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required");

            var request = new ForgotPasswordVm { Email = "" };

            // Act
            var result = await _controller.ForgotPassword(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
