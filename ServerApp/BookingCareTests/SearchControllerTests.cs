using BookingCare.API.Controllers;
using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;

namespace BookingCareTests
{
    [TestFixture]
    public class SearchControllerTests
    {
        private Mock<ISearchService> _mockSearchService;
        private SearchController _searchController;

        [SetUp]
        public void Setup()
        {
            _mockSearchService = new Mock<ISearchService>();
            _searchController = new SearchController( _mockSearchService.Object );
        }

        [Test]
        public async Task SearchBySpecialization_ShouldReturnOk_WhenSearchIsSuccessful()
        {
            // Arrange
            var keyword = "cardiology";

            // Tạo một SearchResultDto giả để trả về từ mock service
            var searchResultDto = new SearchResultDto
            {
                Message = "Search results found",
                Doctors = new List<DoctorSearchDto>
        {
            new DoctorSearchDto { UserId = 1, UserName = "Dr. Smith", Email = "dr.smith@example.com", SpecializationName = "Cardiology", ClinicName = "Heart Clinic" }
        },
                Clinics = new List<ClinicSearchDto>
        {
            new ClinicSearchDto { Id = 1, Name = "Heart Clinic", Address = "123 Cardiology St." }
        },
                Specializations = new List<SpecializationSearchDto>
        {
            new SpecializationSearchDto { Id = 1, Name = "Cardiology", Description = "Heart and blood vessels" }
        }
            };

            // Mock service trả về SearchResultDto khi gọi SearchBySpecializationAsync
            _mockSearchService.Setup(service => service.SearchBySpecializationAsync(keyword))
                .ReturnsAsync(searchResultDto);

            // Khởi tạo controller với mock service
            var searchController = new SearchController(_mockSearchService.Object);

            // Act
            var result = await searchController.SearchBySpecialization(keyword);

            // Assert
            var okResult = result.Result as OkObjectResult;  // result.Result thay vì trực tiếp result
            Assert.IsNotNull(okResult, "Result should be OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200 OK");

            var response = okResult.Value as SearchResultDto;
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual("Search results found", response.Message, "Message should be correct");
            Assert.AreEqual(1, response.Doctors.Count, "Should return 1 doctor");
            Assert.AreEqual(1, response.Clinics.Count, "Should return 1 clinic");
            Assert.AreEqual(1, response.Specializations.Count, "Should return 1 specialization");
        }

        [Test]
        public async Task SearchBySpecialization_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var keyword = "cardiology";

            // Mock service trả về lỗi
            _mockSearchService.Setup(service => service.SearchBySpecializationAsync(keyword))
                .ThrowsAsync(new Exception("Internal server error"));

            // Khởi tạo controller với mock service
            var searchController = new SearchController(_mockSearchService.Object);

            // Act
            var result = await searchController.SearchBySpecialization(keyword);

            // Assert
            var objectResult = result.Result as ObjectResult; // Kết quả có thể là ObjectResult
            Assert.IsNotNull(objectResult, "Result should be ObjectResult");
            Assert.AreEqual(500, objectResult.StatusCode, "Status code should be 500 Internal Server Error");

            var response = objectResult.Value as dynamic;
            Assert.IsNotNull(response, "Response should not be null");

            var parsed = JObject.FromObject(response);
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("An error occurred while processing your request."));
            Assert.That(parsed["Details"].ToString(), Is.EqualTo("Internal server error"));
        }


    }
}
