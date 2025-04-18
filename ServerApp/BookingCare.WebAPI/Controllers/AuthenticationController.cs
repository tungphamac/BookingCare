﻿using BookingCare.Business.ViewModels;
using BookingCare.Data.Data;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingCare.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager; // Thêm RoleManager
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager, // Inject RoleManager
            AppDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVm registerVm)
        {



            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
            }

            var userExists = await _userManager.FindByEmailAsync(registerVm.Email);
            if (userExists != null)
                return BadRequest(new { message = "Email đã tồn tại" });

            var newUser = new User
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                Gender = registerVm.Gender,
                Address = registerVm.Address,
                PhoneNumber = registerVm.Phone,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            //// Kiểm tra và tạo vai trò "User" nếu chưa tồn tại
            //if (!await _roleManager.RoleExistsAsync("User"))
            //{
            //    var role = new IdentityRole<int> { Name = "User" };
            //    await _roleManager.CreateAsync(role);
            //}

            // Gán vai trò "User" cho người dùng mới
            await _userManager.AddToRoleAsync(newUser, "Patient");
            var newPatient = new Patient
            {
                UserId = newUser.Id, // Gán UserId bằng Id của AppUser vừa tạo
            };

            // Lưu vào bảng Patient
            await _context.Patients.AddAsync(newPatient);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"User {registerVm.Email} created successfully with role 'Patient'" });
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }

            var user = await _userManager.FindByEmailAsync(loginVm.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginVm.Password))
            {
                var tokenValue = await GenerateJwtToken(user);

                // Kiểm tra vai trò dựa trên quan hệ với bảng Patient hoặc Doctor
                var isPatient = _context.Patients.Any(p => p.UserId == user.Id);
                var isDoctor = _context.Doctors.Any(d => d.UserId == user.Id);
                var isAdmin = _context.Users.Any(a => a.Id == user.Id);

                string role = isPatient ? "Patient" : isDoctor ? "Doctor" : isAdmin ? "Admin" : "Unknown";

                // Trả về token, email, id và vai trò
                return Ok(new
                {
                    token = tokenValue,
                    email = user.Email,
                    id = user.Id,
                    role = role
                });

            }

            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    }
