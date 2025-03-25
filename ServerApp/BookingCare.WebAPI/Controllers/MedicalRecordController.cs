//using BookingCare.Business.Services;
//using BookingCare.Data.Models;
//using BookingCare.WebAPI.DTOs;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace BookingCare.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MedicalRecordController : ControllerBase
//    {
//        private readonly IMedicalRecordService _medicalRecordService;

//        public MedicalRecordController(IMedicalRecordService medicalRecordService)
//        {
//            _medicalRecordService = medicalRecordService;
//        }

//        [HttpPost]
//        [Authorize(Roles = "Doctor")]
//        public async Task<IActionResult> AddMedicalRecord([FromBody] MedicalRecordDTO dto)
//        {
//            var record = new MedicalRecord
//            {
//                AppointmentId = dto.AppointmentId
//            };
//            var userId = int.Parse(User.Identity.Name);
//            var result = await _medicalRecordService.AddMedicalRecordAsync(record, userId);
//            return Ok(result);
//        }

//        [HttpPut("{id}")]
//        [Authorize(Roles = "Doctor")]
//        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] MedicalRecordDTO dto)
//        {
//            var record = new MedicalRecord
//            {
//                Id = id,
//                AppointmentId = dto.AppointmentId
//            };
//            var userId = int.Parse(User.Identity.Name);
//            var result = await _medicalRecordService.UpdateMedicalRecordAsync(record, userId);
//            return Ok(result);
//        }

//        [HttpGet("{id}")]
//        [Authorize(Roles = "Doctor,Patient")]
//        public async Task<IActionResult> ViewMedicalRecord(int id)
//        {
//            var userId = int.Parse(User.Identity.Name);
//            var record = await _medicalRecordService.ViewMedicalRecordAsync(id, userId);
//            if (record == null) return NotFound();
//            return Ok(record);
//        }
//    }
//}