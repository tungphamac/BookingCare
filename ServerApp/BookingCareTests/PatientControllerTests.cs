using BookingCare.API.Controllers;
using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;

namespace BookingCareTests
{
    public class PatientControllerTests
    {
        private Mock<IPatientService> _mockPatientService;
        private Mock<ILogger<PatientController>> _mockLogger;
        private PatientController _patientController;
        private readonly UserManager<User> _userManager;

        [SetUp]
        public void SetUp()
        {
            _mockPatientService = new Mock<IPatientService>();
            _mockLogger = new Mock<ILogger<PatientController>>();
            _patientController = new PatientController(_mockPatientService.Object, _userManager ,_mockLogger.Object);
        }

        [Test]
        public async Task GetPatientDetail_ShouldReturnOk_WhenPatientExists()
        {
            // Arrange
            var patientDetail = new PatientDetailDto
            {
                Id = 1,
                UserName = "JohnDoe",
                Email = "johndoe@example.com",
                Gender = true,
                Phone = "1234567890",
                Address = "123 Main St",
                Avatar = "avatar.jpg",
                MedicalRecordId = 101
            };

            // Setup mock service to return the patient detail
            _mockPatientService.Setup(service => service.GetPatientDetailAsync(1))
                .ReturnsAsync(patientDetail);

            // Act
            var result = await _patientController.GetPatientDetail(1);

            // Assert
            var okResult = result.Result as OkObjectResult;  
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);  
            Assert.AreEqual(patientDetail, okResult.Value);  
        }

        [Test]
        public async Task LockPatientAccount_ShouldReturnNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            var userId = 1;
            var lockUntil = DateTime.Now.AddDays(7);

            _mockPatientService.Setup(service => service.LockUserAccountAsync(userId, lockUntil))
                .ReturnsAsync(false);  // Không tìm thấy bệnh nhân

            // Act
            var result = await _patientController.LockPatientAccount(userId, lockUntil);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);

            var response = notFoundResult.Value;

            var parsed = JObject.FromObject(response);
            Assert.That(parsed["Message"].ToString(), Is.EqualTo($"User with ID {userId} not found."));
        }

        [Test]
        public async Task LockPatientAccount_ShouldReturnOk_WhenAccountLockedSuccessfully()
        {
            // Arrange
            var userId = 1;
            var lockUntil = DateTime.Now.AddDays(7);

            _mockPatientService.Setup(service => service.LockUserAccountAsync(userId, lockUntil))
                .ReturnsAsync(true);  // Thành công

            // Act
            var result = await _patientController.LockPatientAccount(userId, lockUntil);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value;

            var parsed = JObject.FromObject(response);
            Assert.That(parsed["Message"].ToString(), Is.EqualTo($"User account with ID {userId} has been locked until {lockUntil}."));
        }
        [Test]
        public async Task GetPatients_ReturnsOk_WhenPatientsFound()
        {
            // Arrange
            var patients = new List<PatientDetailDto>
    {
        new PatientDetailDto
        {
            Id = 1,
            UserName = "JohnDoe",
            Email = "john.doe@example.com",
            Gender = true, // Male
            Phone = "123-456-7890",
            Address = "123 Main St, City, Country",
            Avatar = "avatar1.png",
            MedicalRecordId = 101
        },
        new PatientDetailDto
        {
            Id = 2,
            UserName = "JaneDoe",
            Email = "jane.doe@example.com",
            Gender = false, // Female
            Phone = "987-654-3210",
            Address = "456 Elm St, City, Country",
            Avatar = "avatar2.png",
            MedicalRecordId = 102
        }
    };

            _mockPatientService.Setup(service => service.GetAllAsync()).ReturnsAsync(patients);

            // Act
            var result = await _patientController.GetPatients();

            // Assert
            var okResult = result.Result as OkObjectResult;  
            Assert.NotNull(okResult);  
            Assert.AreEqual(200, okResult.StatusCode);  

            var responseContent = okResult.Value as IEnumerable<PatientDetailDto>; 
            Assert.NotNull(responseContent); 
            Assert.AreEqual(2, responseContent.Count()); 

            // Ensure specific properties are correct
            var firstPatient = responseContent.First();
            Assert.AreEqual("JohnDoe", firstPatient.UserName);
            Assert.AreEqual("john.doe@example.com", firstPatient.Email);
            Assert.AreEqual("123-456-7890", firstPatient.Phone);
        }

        [Test]
        public async Task GetPatients_ReturnsNotFound_WhenNoPatientsFound()
        {
            // Arrange
            _mockPatientService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<PatientDetailDto>());

            // Act
            var result = await _patientController.GetPatients();

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);  
            Assert.AreEqual(404, notFoundResult.StatusCode);  

            var responseContent = notFoundResult.Value as dynamic;
            Assert.NotNull(responseContent);  
            var parsed =JObject.FromObject(responseContent);
            Assert.That(parsed["Message"].ToString(), Is.EqualTo("No patients found."));
        }
        [Test]
        public async Task GetPatients_ReturnsStatus500_WhenErrorOccurs()
        {
            _mockPatientService.Setup(service => service.GetAllAsync()).ThrowsAsync(new Exception("Error retrieving patients"));

            var result = await _patientController.GetPatients();

            Assert.NotNull(result); 

            var statusCodeResult = result.Result as ObjectResult; 
            Assert.NotNull(statusCodeResult);  
            Assert.AreEqual(500, statusCodeResult.StatusCode);  
        }
    }
}
