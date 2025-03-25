using BookingCare.Business.Services;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookingCare.Business.DTOs;
using BookingCare.Data.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingCare.WebAPI.Properties
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;
        private readonly UserManager<User> _userManager;

        // Constructor: Tiêm cả DoctorService và UserManager
        public DoctorsController(DoctorService doctorService, UserManager<User> userManager)
        {
            _doctorService = doctorService;
            _userManager = userManager; // Tiêm UserManager vào
        }

        // API GET để lấy tất cả bác sĩ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        // API GET để lấy bác sĩ theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        // API POST để thêm bác sĩ
        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                // Tạo tài khoản người dùng mới từ DTO
                var user = new User
                {
                    UserName = request.User.UserName,
                    Email = request.User.Email,
                    PhoneNumber = request.User.PhoneNumber,
                    Gender = request.User.Gender,
                    Address = request.User.Address,
                    Avatar = request.User.Avatar
                };

                // Sử dụng mật khẩu mạnh hơn
                var password = "Default@123"; // Đảm bảo mật khẩu có ít nhất một chữ số và ký tự đặc biệt

                // Tạo người dùng và xác nhận mật khẩu
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    // Tạo bác sĩ
                    var doctor = new Doctor
                    {
                        UserId = user.Id,
                        Achievement = request.Achievement,
                        Description = request.Description,
                        SpecializationId = request.SpecializationId,
                        ClinicId = request.ClinicId
                    };

                    // Thêm bác sĩ vào cơ sở dữ liệu
                    await _doctorService.AddDoctorAsync(doctor);

                    return Ok("Doctor added successfully.");
                }
                else
                {
                    // Trả về các lỗi chi tiết
                    string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest($"Error creating user: {errors}");
                }
            }
            return BadRequest("Invalid data.");
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDoctor(int id, [FromBody] DoctorCreateRequest request)
        //{
        //    if (id != request.User.UserId)
        //    {
        //        return BadRequest("Doctor ID mismatch.");
        //    }

        //    // Kiểm tra nếu bác sĩ tồn tại
        //    var doctor = await _doctorService.GetDoctorByIdAsync(id);
        //    if (doctor == null)
        //    {
        //        return NotFound("Doctor not found.");
        //    }

        //    // Cập nhật thông tin người dùng (User) mà không thay đổi UserName và Password
        //    var user = doctor.User;

        //    user.Email = request.User.Email ?? user.Email;
        //    user.PhoneNumber = request.User.PhoneNumber ?? user.PhoneNumber;
        //    user.Gender = request.User.Gender ?? user.Gender;
        //    user.Address = request.User.Address ?? user.Address;
        //    user.Avatar = request.User.Avatar ?? user.Avatar;

        //    // Cập nhật các thông tin bác sĩ (Doctor)
        //    doctor.Achievement = request.Achievement ?? doctor.Achievement;
        //    doctor.Description = request.Description ?? doctor.Description;
        //    doctor.SpecializationId = request.SpecializationId ?? doctor.SpecializationId;
        //    doctor.ClinicId = request.ClinicId ?? doctor.ClinicId;

        //    try
        //    {
        //        // Cập nhật bác sĩ
        //        await _doctorService.UpdateDoctorAsync(id, doctor);
        //        return NoContent(); // Trả về 204 khi cập nhật thành công
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error updating doctor: {ex.Message}");
        //    }
        //}


        // API DELETE để xóa bác sĩ
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                await _doctorService.DeleteDoctorAsync(id);
                return NoContent(); // Trả về 204 khi xóa thành công
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Trả về 404 nếu không tìm thấy bác sĩ
            }
        }
        // API PUT để khóa bác sĩ
        [HttpPut("lock/{id}")]
        public async Task<IActionResult> LockDoctor(int id)
        {
            try
            {
                await _doctorService.LockDoctorAsync(id);
                return Ok("Doctor account has been locked.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về thông báo lỗi nếu tài khoản đã bị khóa
            }
            catch (Exception ex)
            {
                return BadRequest($"Error locking doctor account: {ex.Message}");
            }
        }

        // API PUT để mở khóa bác sĩ
        [HttpPut("unlock/{id}")]
        public async Task<IActionResult> UnlockDoctor(int id)
        {
            try
            {
                await _doctorService.UnlockDoctorAsync(id);
                return Ok("Doctor account has been unlocked.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error unlocking doctor account: {ex.Message}");
            }
        }
    }
}