using BookingCare.Business.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using BookingCare.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace BookingCareTests
{
    [TestFixture]
    public class MedicalRecordControllerTests
    {
        private Mock<IMedicalRecordService> _mockMedicalRecordService;
        private Mock<ILogger<MedicalRecordController>> _mockLogger;
        private MedicalRecordController _medicalRecordController;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        [SetUp]
        public void SetUp()
        {
            _mockMedicalRecordService = new Mock<IMedicalRecordService>();
            _mockLogger = new Mock<ILogger<MedicalRecordController>>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _unitOfWork = new Mock<IUnitOfWork>(); // Initialize the unitOfWork mock

            // Mock the User's Claim for user ID
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.NameIdentifier, "1"), // Mock user ID as 1
            }));

            _mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(mockHttpContext);

            _medicalRecordController = new MedicalRecordController(
                _mockMedicalRecordService.Object,
                _unitOfWork.Object,  // Pass the mock here
                _mockLogger.Object
            );
        }
    }
}
