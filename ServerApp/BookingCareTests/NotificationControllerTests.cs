using BookingCare.API.Controllers;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;

namespace BookingCareTests
{
    [TestFixture]
    public class NotificationControllerTests
    {
        private Mock<INotificationService> _mockNotificationService;
        private Mock<ILogger<NotificationController>> _mockLogger;
        private NotificationController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockNotificationService = new Mock<INotificationService>();
            _mockLogger = new Mock<ILogger<NotificationController>>();
            _controller = new NotificationController(_mockNotificationService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetNotifications_ReturnsBadRequest_WhenUserIdIsInvalid()
        {
            // Arrange
            int invalidUserId = 0;

            // Act
            var result = await _controller.GetNotifications(invalidUserId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Invalid user ID.", badRequestResult.Value);
        }
        
    }
}
