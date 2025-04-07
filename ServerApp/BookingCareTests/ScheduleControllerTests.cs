

using BookingCare.API.Controllers;
using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;

namespace BookingCareTests
{
    [TestFixture]
    public class ScheduleControllerTests
    {
        private Mock<IScheduleService> _mockScheduleService;
        private Mock<ILogger<ScheduleController>> _mockLogger;
        private ScheduleController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockScheduleService = new Mock<IScheduleService>();
            _mockLogger = new Mock<ILogger<ScheduleController>>();
            _controller = new ScheduleController(_mockScheduleService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetScheduleById_ExistingId_ReturnsOk()
        {
            // Arrange
            var schedule = new ScheduleDetailDto
            {
                Id = 1,
                DoctorId = 10,
                TimeSlot = "08:00 - 09:00",
                WorkDate = new DateTime(2025, 4, 7),
                Status = "Available"
            };

            _mockScheduleService.Setup(s => s.GetScheduleByIdAsync(1))
                                .ReturnsAsync(schedule);

            // Act
            var result = await _controller.GetScheduleById(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(schedule, okResult.Value);
        }

        [Test]
        public async Task GetScheduleById_NotFound_ReturnsNotFound()
        {
            // Arrange
            _mockScheduleService.Setup(s => s.GetScheduleByIdAsync(999))
                                .ReturnsAsync((ScheduleDetailDto)null);

            // Act
            var result = await _controller.GetScheduleById(999);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult.Value.ToString(), Does.Contain("Schedule with ID 999 not found"));
        }
        [Test]
        public async Task GetScheduleById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _mockScheduleService.Setup(s => s.GetScheduleByIdAsync(It.IsAny<int>()))
                                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetScheduleById(5);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.That(objectResult.Value.ToString(), Does.Contain("An error occurred"));
        }

        [Test]
        public async Task GetAllSchedules_ReturnsOkWithSchedules()
        {
            // Arrange
            var mockSchedules = new List<ScheduleDetailDto>
            {
                new ScheduleDetailDto { Id = 1, DoctorId = 101, TimeSlot = "08:00 - 09:00", WorkDate = DateTime.Today, Status = "Available" },
                new ScheduleDetailDto { Id = 2, DoctorId = 102, TimeSlot = "09:00 - 10:00", WorkDate = DateTime.Today, Status = "Booked" }
            };

            _mockScheduleService.Setup(s => s.GetAllSchedulesAsync())
                                .ReturnsAsync(mockSchedules);

            // Act
            var result = await _controller.GetAllSchedules();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(mockSchedules, okResult.Value);
        }
        [Test]
        public async Task GetAllSchedules_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _mockScheduleService.Setup(s => s.GetAllSchedulesAsync())
                                .ThrowsAsync(new Exception("Database failure"));

            // Act
            var result = await _controller.GetAllSchedules();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("An error occurred while retrieving schedules.", objectResult.Value);
        }


        [Test]
        public async Task DeleteSchedule_ReturnsStatus500_WhenUnhandledExceptionThrown()
        {
            // Arrange
            int scheduleId = 3;

            _mockScheduleService.Setup(s => s.DeleteScheduleAsync(scheduleId))
                                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.DeleteSchedule(scheduleId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;

            Assert.AreEqual(500, objectResult!.StatusCode);
            Assert.AreEqual("An error occurred while deleting the schedule.", objectResult.Value);
        }
    }
}
