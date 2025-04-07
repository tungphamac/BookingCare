using BookingCare.API.Controllers;
using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;

namespace BookingCareTests
{
    [TestFixture]
    public class DoctorControllerTests
    {
        private Mock<IDoctorService> _mockDoctorService;
        private Mock<ILogger<DoctorController>> _mockLogger;
        private DoctorController _doctorController;

        [SetUp]
        public void Setup()
        {
            _mockDoctorService = new Mock<IDoctorService>();
            _mockLogger = new Mock<ILogger<DoctorController>>();
            _doctorController = new DoctorController(_mockDoctorService.Object, _mockLogger.Object);
        }

        //Test GetDoctorById()
        [Test]
        public async Task GetDoctorById_ValidId_ReturnOkResult_WithDoctor()
        {
            //arrange
            var doctorId = 1;
            var mockDoctor = new DoctorVm
            {
                Id = 1,
                Name = "nam",
                Gender = true,
                Email = "nbam@gmail.com",
                Phone = "3818238131",
                Address = "hoalac",
                Avatar = "avatar.png",
                Achievement = "abc",
                Description = "cba",
                SpecializationId = 1,
                ClinicId = 1
            };
            _mockDoctorService.Setup(d => d.GetDoctorByIdAsync(doctorId)).ReturnsAsync(mockDoctor);


            //action
            var result = await _doctorController.GetDoctorById(doctorId);

            //assert

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value;
            Assert.AreEqual(mockDoctor, response);
        }


        [Test]
        public async Task GetDoctorById_ReturnsNotFound_WhenDoctorDoesNotExist()
        {
            // Arrange
            var doctorId = 2;
            _mockDoctorService.Setup(s => s.GetDoctorByIdAsync(doctorId)).ReturnsAsync((DoctorVm)null);

            // Act
            var result = await _doctorController.GetDoctorById(doctorId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetAllDoctors_ReturnsOk_WithListOfDoctors()
        {
            // Arrange
            var mockDoctors = new List<DoctorDetailDto>
            {
                new DoctorDetailDto {
                Id = 1,
                UserName = "bam",
                Gender = true,
                Email = "nbam@gmail.com",
                Address = "hoalac",
                Avatar = "avatar.png",
                Achievement = "abc",
                Description = "cba",
                SpecializationName = "a",
                ClinicName = "b"
            },
                new DoctorDetailDto {
                Id = 2,
                UserName = "nM",
                Gender = true,
                Email = "nbam@gmail.com",
                Address = "hoalac",
                Avatar = "avatar.png",
                Achievement = "abc",
                Description = "cba",
                SpecializationName = "a",
                ClinicName = "b"
                }
            };
            _mockDoctorService.Setup(s => s.GetAllDoctorsAsync()).ReturnsAsync(mockDoctors);

            // Act
            var result = await _doctorController.GetAllDoctors();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<List<DoctorDetailDto>>(okResult.Value);
            Assert.AreEqual(2, ((List<DoctorDetailDto>)okResult.Value).Count);
        }

        [Test]
        public async Task CreateDoctor_ReturnsOk_WithDoctorId()
        {
            // Arrange
            var dto = new CreateDoctorDto
            {
                UserName = "doctor.username",
                Email = "doctor@example.com",
                Password = "P@ssw0rd",
                Gender = true,
                Address = "123 ABC Street",
                Avatar = "avatar.jpg",
                Achievement = "Top Neurologist",
                Description = "Experienced in brain surgery",
                SpecializationId = 1,
                ClinicId = 2
            };

            _mockDoctorService.Setup(s => s.CreateDoctorAsync(dto)).ReturnsAsync(123);

            // Act
            var result = await _doctorController.CreateDoctor(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var parsed = JObject.FromObject(okResult.Value);
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("Doctor created successfully."));
            Assert.That((int)parsed["DoctorId"], Is.EqualTo(123));
        }

        [Test]
        public async Task UpdateDoctor_ReturnsOk_WhenDoctorUpdatedSuccessfully()
        {
            // Arrange
            var doctorUpdateDto = new DoctorUpdateDto
            {
                UserName = "UpdatedDoctor",
                Email = "updatedoctor@example.com",
                Gender = true,
                Address = "456 XYZ Street",
                Avatar = "updated-avatar.jpg",
                Achievement = "New Achievements",
                Description = "Updated Doctor Description",
                SpecializationId = 2,
                ClinicId = 3
            };

            var doctorId = 1;

            // Mock the service method that updates the doctor
            _mockDoctorService.Setup(s => s.UpdateDoctorAsync(doctorId, doctorUpdateDto)).ReturnsAsync(true);

            // Act
            var result = await _doctorController.UpdateDoctor(doctorId, doctorUpdateDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            // Kiểm tra lại cấu trúc của object trả về
            var response = okResult.Value as dynamic;
            Assert.AreEqual("Doctor updated successfully.", response);
        }

        [Test]
        public async Task UpdateDoctor_ReturnsNotFound_WhenDoctorDoesNotExist()
        {
            // Arrange
            var doctorUpdateDto = new DoctorUpdateDto
            {
                UserName = "NonExistentDoctor",
                Email = "nonexistentdoctor@example.com",
                Gender = true,
                Address = "789 DEF Street",
                Avatar = "nonexistent-avatar.jpg",
                Achievement = "No Achievements",
                Description = "This doctor does not exist.",
                SpecializationId = 4,
                ClinicId = 5
            };

            var doctorId = 999;  // Giả sử bác sĩ có ID này không tồn tại

            // Mock the service method that checks for doctor existence
            _mockDoctorService.Setup(s => s.UpdateDoctorAsync(doctorId, doctorUpdateDto)).ReturnsAsync(false);

            // Act
            var result = await _doctorController.UpdateDoctor(doctorId, doctorUpdateDto);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual($"Doctor with ID {doctorId} not found.", notFoundResult.Value);
        }

        [Test]
        public async Task LockDoctorAccount_ReturnsOk_WhenAccountLockedSuccessfully()
        {
            // Arrange
            int doctorId = 1;
            DateTime lockUntil = DateTime.Now.AddDays(7);  // Lock tài khoản trong 7 ngày tới

            // Mock phương thức LockUserAccountAsync trả về true (người dùng được khóa thành công)
            _mockDoctorService.Setup(s => s.LockUserAccountAsync(doctorId, lockUntil)).ReturnsAsync(true);

            // Act
            var result = await _doctorController.LockDoctorAccount(doctorId, lockUntil);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);  // Kiểm tra nếu kết quả là OkObjectResult
            var okResult = result as OkObjectResult;
            var response = okResult.Value as dynamic;
            var parsed = JObject.FromObject(response);
            // Kiểm tra message trả về
            Assert.That(parsed["Message"].ToString(), Is.EqualTo($"User account with ID {doctorId} has been locked until {lockUntil}."));
        }

        [Test]
        public async Task LockDoctorAccount_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            int doctorId = 1;
            DateTime lockUntil = DateTime.Now.AddDays(7);  // Lock tài khoản trong 7 ngày tới

            // Mock phương thức LockUserAccountAsync gây lỗi
            _mockDoctorService.Setup(s => s.LockUserAccountAsync(doctorId, lockUntil)).ThrowsAsync(new Exception("An error occurred while locking the account."));

            // Act
            var result = await _doctorController.LockDoctorAccount(doctorId, lockUntil);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result); // Kiểm tra kiểu trả về là ObjectResult
            var objectResult = result as ObjectResult;

            // Kiểm tra mã trạng thái HTTP là 500
            Assert.AreEqual(500, objectResult.StatusCode);

            // Kiểm tra nội dung phản hồi
            var response = objectResult.Value as dynamic;
            Assert.IsNotNull(response);
            Assert.That(response, Is.EqualTo("An error occurred while locking the account."));
        }
    }
}
