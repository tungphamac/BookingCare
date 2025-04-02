using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        // Thư mục upload ảnh
        private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public UploadController()
        {
            // Kiểm tra và tạo thư mục nếu chưa có
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Tạo tên file mới để tránh trùng
            var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(_uploadDirectory, fileName);

            // Lưu file vào thư mục uploads
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về URL của ảnh đã tải lên
            var fileUrl = $"http://localhost:5000/uploads/{fileName}";

            return Ok(new { url = fileUrl });
        }
    }

}
