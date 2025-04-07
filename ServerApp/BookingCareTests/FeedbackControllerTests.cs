using BookingCare.API.Controllers;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace BookingCareTests
{
    [TestFixture]
    public class FeedbackControllerTests
    {
        private Mock<IFeedbackService> _mockFeedbackService;
        private Mock<ILogger<FeedbackController>> _mockLogger;
        private FeedbackController _feedbackController; 

        [SetUp]
        public void SetUp()
        {
            _mockFeedbackService = new Mock<IFeedbackService>();
            _mockLogger = new Mock<ILogger<FeedbackController>>();
            _feedbackController = new FeedbackController(_mockFeedbackService.Object, _mockLogger.Object);
        }

        // Test GetAllFeedbacks
        [Test]
        public async Task GetAllFeedbacks_ReturnsOkResult_WithFeedbackList()
        {
            // Arrange
            var mockFeedbacks = new List<FeedbackVm>
            {
                new FeedbackVm { Id = 1, PatientName = "John Doe", AppointmentId = 1, Rating = 5, Comment = "Great service", CreateAt = new DateTime(2025, 4, 6, 14, 11, 53) },
                new FeedbackVm { Id = 2, PatientName = "Jane Smith", AppointmentId = 2, Rating = 4, Comment = "Very helpful", CreateAt = new DateTime(2025, 4, 6, 14, 11, 53) }
            };

            _mockFeedbackService.Setup(x => x.GetAllFeedbacksAsync()).ReturnsAsync(mockFeedbacks);

            // Act
            var result = await _feedbackController.GetAllFeedbacks();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); // Kiểm tra mã trạng thái HTTP
            var response = okResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.AreEqual(mockFeedbacks, response); // Kiểm tra dữ liệu trả về
        }

        // Test GetFeedbackById (valid id)
        [Test]
        public async Task GetFeedbackById_ValidId_ReturnsOkResult_WithFeedback()
        {
            // Arrange
            var feedbackId = 1;
            var mockFeedback = new FeedbackVm { Id = feedbackId, PatientName = "John Doe", AppointmentId = 1, Rating = 5, Comment = "Great service!" };

            _mockFeedbackService.Setup(x => x.GetFeedbackByIdAsync(feedbackId)).ReturnsAsync(mockFeedback);

            // Act
            var result = await _feedbackController.GetFeedbackById(feedbackId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); // Kiểm tra mã trạng thái HTTP
            var response = okResult.Value;
            Assert.AreEqual(mockFeedback, response); // Kiểm tra dữ liệu trả về
        }

        // Test GetFeedbackById (invalid id)
        [Test]
        public async Task GetFeedbackById_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var feedbackId = 99;
            _mockFeedbackService.Setup(x => x.GetFeedbackByIdAsync(feedbackId)).ThrowsAsync(new ArgumentException("Feedback not found"));

            // Act
            var result = await _feedbackController.GetFeedbackById(feedbackId);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode); // Kiểm tra mã trạng thái HTTP
            var response = badRequestResult.Value as dynamic;
            Assert.AreEqual("Feedback not found", response); // Kiểm tra message
        }

        // Test AddFeedback (valid input)
        [Test]
        public async Task AddFeedback_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var feedbackVm = new FeedbackVm { PatientName = "John Doe", AppointmentId = 1, Rating = 5, Comment = "Excellent service!" };
            var userId = 1;

            _mockFeedbackService.Setup(x => x.AddFeedbackAsync(feedbackVm, userId)).ReturnsAsync(true);

            var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) });
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _feedbackController.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _feedbackController.AddFeedback(feedbackVm);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); // Kiểm tra mã trạng thái HTTP
            var response = okResult.Value as dynamic;
            Assert.AreEqual("Feedback added successfully.", response); // Kiểm tra message
        }

        // Test AddFeedback (Unauthorized - Missing or invalid user ID)
        [Test]
        public async Task AddFeedback_MissingOrInvalidUserId_ReturnsUnauthorized()
        {
            // Arrange
            var feedbackVm = new FeedbackVm { PatientName = "John Doe", AppointmentId = 1, Rating = 5, Comment = "Excellent service!" };

            var claimsIdentity = new ClaimsIdentity(); // Không có Claim
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _feedbackController.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = await _feedbackController.AddFeedback(feedbackVm);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.NotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode); // Kiểm tra mã trạng thái HTTP
            var response = unauthorizedResult.Value as dynamic;
            Assert.AreEqual("Invalid or missing user ID.", response); // Kiểm tra message
        }

        // Test UpdateFeedback (valid update)
        [Test]
        public async Task UpdateFeedback_ValidUpdate_ReturnsOkResult()
        {
            // Arrange
            var feedbackId = 1;
            var updateFeedbackVm = new UpdateFeedbackVm { Comment = "Updated feedback" };
            _mockFeedbackService.Setup(x => x.UpdateFeedback(feedbackId, updateFeedbackVm)).ReturnsAsync(true);

            // Act
            var result = await _feedbackController.UpdateFeedback(feedbackId, updateFeedbackVm);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); // Kiểm tra mã trạng thái HTTP
            var response = okResult.Value as dynamic;
            Assert.AreEqual("Feedback updated successfully.", response); // Kiểm tra message
        }

        // Test DeleteFeedback (valid id)
        [Test]
        public async Task DeleteFeedback_ValidId_ReturnsOkResult()
        {
            // Arrange
            var feedbackId = 1;
            _mockFeedbackService.Setup(x => x.DeleteFeedbackAsync(feedbackId)).ReturnsAsync(true);

            // Act
            var result = await _feedbackController.DeleteFeedback(feedbackId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); // Kiểm tra mã trạng thái HTTP
            var response = okResult.Value as dynamic;
            Assert.AreEqual("Feedback deleted successfully."    , response); // Kiểm tra message
        }
    }
}