using Azure;
using BookingCare.API.Controllers;
using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCareTests
{
    [TestFixture]
    public class ClinicControllerTests
    {
        private Mock<IClinicService> _mockClinicService;
        private Mock<ILogger<ClinicController>> _mockLogger;
        private ClinicController _clinicController;

        [SetUp]
        public void SetUp()
        {
            _mockClinicService = new Mock<IClinicService>();
            _mockLogger = new Mock<ILogger<ClinicController>>();
            _clinicController = new ClinicController(_mockClinicService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetClinicById_ShouldReturnOk_WhenClinicExists()
        {
            // Arrange
            int clinicId = 1;
            var clinicDto = new ClinicDetailDto
            {
                Id = clinicId,
                Name = "Test Clinic",
                Address = "123 Test St",
                Phone = 123456789,
                Introduction = "A test clinic",
                CreateAt = DateTime.Now
            };
            _mockClinicService.Setup(service => service.GetClinicByIdAsync(clinicId))
                .ReturnsAsync(clinicDto);

            // Act
            var result = await _clinicController.GetClinicById(clinicId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(clinicDto, okResult.Value);
        }

        [Test]
        public async Task GetClinicById_ShouldReturnNotFound_WhenClinicDoesNotExist()
        {
            // Arrange
            int clinicId = 1;
            _mockClinicService.Setup(service => service.GetClinicByIdAsync(clinicId))
                .ThrowsAsync(new ArgumentException("Clinic not found"));

            // Act
            var result = await _clinicController.GetClinicById(clinicId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);

            // Kiểm tra giá trị của Message trong notFoundResult.Value
            dynamic responseValue = notFoundResult.Value;
            var parsed = JObject.FromObject(responseValue);
            Assert.IsNotNull(responseValue);
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("Clinic not found"));
        }

        [Test]
        public async Task GetAllClinics_ShouldReturnOk_WhenClinicsExist()
        {
            // Arrange
            var clinicList = new List<ClinicDetailDto>
        {
            new ClinicDetailDto
            {
                Id = 1,
                Name = "Test Clinic 1",
                Address = "123 Test St",
                Phone = 123456789,
                Introduction = "A test clinic",
                CreateAt = DateTime.Now
            },
            new ClinicDetailDto
            {
                Id = 2,
                Name = "Test Clinic 2",
                Address = "456 Another St",
                Phone = 987654321,
                Introduction = "Another test clinic",
                CreateAt = DateTime.Now
            }
        };

            _mockClinicService.Setup(service => service.GetAllClinicsAsync())
                .ReturnsAsync(clinicList);

            // Act
            var result = await _clinicController.GetAllClinics();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(clinicList, okResult.Value);
        }

        [Test]
        public async Task CreateClinic_ShouldReturnOk_WhenClinicIsCreatedSuccessfully()
        {
            // Arrange
            var clinicDto = new ClinicDetailDto
            {
                Name = "Test Clinic",
                Address = "123 Test St",
                Phone = 123456789,
                Introduction = "A test clinic",
                CreateAt = DateTime.Now
            };

            _mockClinicService.Setup(service => service.CreateClinicAsync(clinicDto))
                .Returns(Task.CompletedTask); // Mô phỏng việc tạo clinic thành công

            // Act
            var result = await _clinicController.CreateClinic(clinicDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value;
            var parsed = JObject.FromObject(response);
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("Clinic created successfully."));
        }

        [Test]
        public async Task GetTopClinics_ShouldReturnOk_WhenClinicsExist()
        {
            // Arrange
            var clinicList = new List<ClinicVm>
        {
            new ClinicVm
            {
                Id = 1,
                Name = "Top Clinic 1",
                Address = "123 Top St",
                Phone = 123456789,
                Introduction = "A top clinic",
                CreateAt = DateTime.Now
            },
            new ClinicVm
            {
                Id = 2,
                Name = "Top Clinic 2",
                Address = "456 Top St",
                Phone = 987654321,
                Introduction = "Another top clinic",
                CreateAt = DateTime.Now
            }
        };

            _mockClinicService.Setup(service => service.GetTopClinics(3))
                .ReturnsAsync(clinicList);

            // Act
            var result = await _clinicController.GetTopClinics();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(clinicList, okResult.Value);
        }

        [Test]
        public async Task GetDoctorsByClinicId_ShouldReturnOk_WhenDoctorsExist()
        {
            // Arrange
            var doctorList = new List<DoctorDetailDto>
        {
            new DoctorDetailDto
            {
                Id = 1,
                UserName = "Dr. Smith",
                Email = "dr.smith@example.com",
                Gender = true,
                Address = "123 Clinic St",
                Avatar = "avatar1.png",
                Achievement = "Award-winning doctor",
                Description = "Experienced in general practice",
                SpecializationName = "General Medicine",
                ClinicName = "Top Clinic"
            },
            new DoctorDetailDto
            {
                Id = 2,
                UserName = "Dr. Johnson",
                Email = "dr.johnson@example.com",
                Gender = false,
                Address = "456 Clinic St",
                Avatar = "avatar2.png",
                Achievement = "Specialist in surgery",
                Description = "Experienced in surgery and trauma care",
                SpecializationName = "Surgery",
                ClinicName = "Top Clinic"
            }
        };

            _mockClinicService.Setup(service => service.GetDoctorsByClinicIdAsync(1))
                .ReturnsAsync(doctorList);

            // Act
            var result = await _clinicController.GetDoctorsByClinicId(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(doctorList, okResult.Value);
        }
        [Test]
        public async Task GetClinicsBySpecializationId_ShouldReturnOk_WhenClinicsExist()
        {
            // Arrange
            var clinicList = new List<ClinicDetailDto>
        {
            new ClinicDetailDto
            {
                Id = 1,
                Name = "Clinic A",
                Address = "123 Main St",
                Phone = 123456789,
                Introduction = "Specializes in general medicine.",
                CreateAt = DateTime.Now
            },
            new ClinicDetailDto
            {
                Id = 2,
                Name = "Clinic B",
                Address = "456 Elm St",
                Phone = 987654321,
                Introduction = "Specializes in cardiology.",
                CreateAt = DateTime.Now
            }
        };

            _mockClinicService.Setup(service => service.GetClinicsBySpecializationIdAsync(1))
                .ReturnsAsync(clinicList);

            // Act
            var result = await _clinicController.GetClinicsBySpecializationId(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(clinicList, okResult.Value);
        }
    }
}
