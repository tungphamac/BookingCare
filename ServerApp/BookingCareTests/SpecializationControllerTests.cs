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
    public class SpecializationControllerTests
    {
        private Mock<ISpecializationService> _mockSpecializationService;
        private Mock<ILogger<SpecializationController>> _mockLogger;
        private SpecializationController _specializationController;

        [SetUp]
        public void Setup()
        {
            _mockSpecializationService = new Mock<ISpecializationService>();
            _mockLogger = new Mock<ILogger<SpecializationController>>();
            _specializationController = new SpecializationController(_mockSpecializationService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetAllSpecializations_ShouldReturnOk_WhenSpecializationsExist()
        {
            // Arrange
            var specializations = new List<SpecializationDetailDto>
    {
        new SpecializationDetailDto { Id = 1, Name = "Cardiology", Description = "Heart and blood vessels", Image = "cardiology.jpg" },
        new SpecializationDetailDto { Id = 2, Name = "Neurology", Description = "Nervous system", Image = "neurology.jpg" }
    };

            // Mock service để trả về danh sách các Specializations
            _mockSpecializationService.Setup(service => service.GetAllSpecializationsAsync())
                .ReturnsAsync(specializations);

            // Act
            var result = await _specializationController.GetAllSpecializations();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be OkObjectResult");

            var response = okResult.Value as dynamic;
            Assert.AreEqual(specializations, response);
        }

        [Test]
        public async Task CreateSpecialization_ShouldReturnOk_WhenSpecializationIsCreatedSuccessfully()
        {
            // Arrange
            var specializationDto = new SpecializationDetailDto
            {
                Name = "Cardiology",
                Description = "Heart and blood vessels",
                Image = "cardiology.jpg"
            };

            var specializationId = 1; // Mocked ID returned by the service

            // Mock service trả về ID của specialization khi gọi CreateSpecializationAsync
            _mockSpecializationService.Setup(service => service.CreateSpecializationAsync(specializationDto))
                .ReturnsAsync(specializationId);

            // Act
            var result = await _specializationController.CreateSpecialization(specializationDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200 OK");

            var response = okResult.Value as dynamic;

            var parsed = JObject.FromObject(response);
            Assert.IsNotNull(response, "Response should not be null");
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("Specialization created successfully."));
            Assert.That(parsed["SpecializationId"].ToString(), Is.EqualTo(specializationId.ToString()));
        }

        [Test]
        public async Task UpdateSpecialization_ShouldReturnOk_WhenSpecializationIsUpdatedSuccessfully()
        {
            // Arrange
            var specializationId = 1;
            var specializationDto = new SpecializationDetailDto
            {
                Name = "Updated Cardiology",
                Description = "Updated Heart and blood vessels",
                Image = "updated_cardiology.jpg"
            };

            // Mock service trả về true khi cập nhật thành công
            _mockSpecializationService.Setup(service => service.UpdateSpecializationAsync(specializationId, specializationDto))
                .ReturnsAsync(true);

            // Act
            var result = await _specializationController.UpdateSpecialization(specializationId, specializationDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200 OK");

            var response = okResult.Value as dynamic;
            var parsed = JObject.FromObject(response);
            Assert.IsNotNull(response, "Response should not be null");
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("Specialization updated successfully."));
        }

        [Test]
        public async Task UpdateSpecialization_ShouldReturnNotFound_WhenSpecializationDoesNotExist()
        {
            // Arrange
            var specializationId = 1;
            var specializationDto = new SpecializationDetailDto
            {
                Name = "Cardiology",
                Description = "Heart and blood vessels",
                Image = "cardiology.jpg"
            };

            // Mock service trả về false khi không tìm thấy specialization
            _mockSpecializationService.Setup(service => service.UpdateSpecializationAsync(specializationId, specializationDto))
                .ReturnsAsync(false);

            // Act
            var result = await _specializationController.UpdateSpecialization(specializationId, specializationDto);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult, "Result should be NotFoundObjectResult");
            Assert.AreEqual(404, notFoundResult.StatusCode, "Status code should be 404 Not Found");

            var response = notFoundResult.Value as dynamic;
            var parsed = JObject.FromObject(response);
            Assert.IsNotNull(response, "Response should not be null");
            Assert.That(parsed["Message"].ToString(), Is.EqualTo($"Specialization with ID {specializationId} not found."));
        }
        [Test]
        public async Task DeleteSpecialization_ShouldReturnOk_WhenSpecializationIsDeletedSuccessfully()
        {
            // Arrange
            var specializationId = 1;

            // Mock service trả về true khi xóa thành công
            _mockSpecializationService.Setup(service => service.DeleteSpecializationAsync(specializationId))
                .ReturnsAsync(true);

            // Act
            var result = await _specializationController.DeleteSpecialization(specializationId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200 OK");

            var response = okResult.Value as dynamic;
            var parsed = JObject.FromObject(response);
            Assert.IsNotNull(response, "Response should not be null");
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("Specialization deleted successfully."));
        }

        [Test]
        public async Task DeleteSpecialization_ShouldReturnNotFound_WhenSpecializationDoesNotExist()
        {
            // Arrange
            var specializationId = 1;

            // Mock service trả về false khi không tìm thấy specialization
            _mockSpecializationService.Setup(service => service.DeleteSpecializationAsync(specializationId))
                .ReturnsAsync(false);

            // Act
            var result = await _specializationController.DeleteSpecialization(specializationId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult, "Result should be NotFoundObjectResult");
            Assert.AreEqual(404, notFoundResult.StatusCode, "Status code should be 404 Not Found");

            var response = notFoundResult.Value as dynamic;
            var parsed = JObject.FromObject(response);
            Assert.IsNotNull(response, "Response should not be null");
            Assert.That(parsed["Message"].ToString(), Is.EqualTo($"Specialization with ID {specializationId} not found."));
        }

        [Test]
        public async Task DeleteSpecialization_ShouldReturnBadRequest_WhenInvalidOperationExceptionOccurs()
        {
            // Arrange
            var specializationId = 1;

            // Mock service ném ra ngoại lệ InvalidOperationException
            _mockSpecializationService.Setup(service => service.DeleteSpecializationAsync(specializationId))
                .ThrowsAsync(new InvalidOperationException("Invalid operation"));

            // Act
            var result = await _specializationController.DeleteSpecialization(specializationId);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Result should be BadRequestObjectResult");
            Assert.AreEqual(400, badRequestResult.StatusCode, "Status code should be 400 Bad Request");

            var response = badRequestResult.Value as dynamic;
            var parsed = JObject.FromObject(response);
            Assert.IsNotNull(response, "Response should not be null");
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("Invalid operation"));
        }

        [Test]
        public async Task GetTopSpecializations_ShouldReturnOk_WhenSpecializationsExist()
        {
            // Arrange
            var topSpecializations = new List<SpecializationDetailDto>
    {
        new SpecializationDetailDto { Id = 1, Name = "Cardiology", Description = "Heart and blood vessels", Image = "cardiology.jpg" },
        new SpecializationDetailDto { Id = 2, Name = "Neurology", Description = "Brain and nervous system", Image = "neurology.jpg" },
        new SpecializationDetailDto { Id = 3, Name = "Orthopedics", Description = "Bones and joints", Image = "orthopedics.jpg" }
    };

            // Mock service trả về danh sách top 3 specializations
            _mockSpecializationService.Setup(service => service.GetTopSpecializationsAsync(3))
                .ReturnsAsync(topSpecializations);

            // Act
            var result = await _specializationController.GetTopSpecializations();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200 OK");

            var response = okResult.Value as List<SpecializationDetailDto>;
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(3, response.Count, "Should return 3 specializations");
            Assert.AreEqual("Cardiology", response[0].Name, "First specialization should be Cardiology");
            Assert.AreEqual("Neurology", response[1].Name, "Second specialization should be Neurology");
            Assert.AreEqual("Orthopedics", response[2].Name, "Third specialization should be Orthopedics");
        }
    }
}
