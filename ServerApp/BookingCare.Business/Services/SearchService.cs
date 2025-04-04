using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SearchService> _logger;

        public SearchService(IUnitOfWork unitOfWork, ILogger<SearchService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<SearchResultDto> SearchPatientsForDoctorAsync(int doctorId, string keyword)
        {
            try
            {
                var result = new SearchResultDto();
                keyword = keyword?.Trim().ToLower() ?? string.Empty;

                // Tìm kiếm bệnh nhân đã đặt lịch hẹn với bác sĩ
                var patients = await _unitOfWork.AppointmentRepository
                    .GetQuery()
                    .Where(a => a.DoctorId == doctorId) // Lọc theo bác sĩ
                    .Include(a => a.Patient)
                        .ThenInclude(p => p.User)
                    .Where(a => string.IsNullOrEmpty(keyword) ||
                                a.Patient.User.UserName.ToLower().Contains(keyword) ||
                                a.Patient.User.Email.ToLower().Contains(keyword) ||
                                a.Patient.User.Address.ToLower().Contains(keyword))
                    .Select(a => new PatientDetailDto
                    {
                        
                        UserName = a.Patient.User.UserName,
                        Email = a.Patient.User.Email,
                        Gender = a.Patient.User.Gender,
                        Address = a.Patient.User.Address,
                        Avatar = a.Patient.User.Avatar,
                        MedicalRecordId = a.Patient.MedicalRecordId
                    })
                    .Distinct() // Tránh trùng lặp bệnh nhân
                    .ToListAsync();

                result.Patients = patients; // Thêm danh sách bệnh nhân vào SearchResultDto
                result.Message = patients.Any() ? "Patients found successfully." : "No patients found.";

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred during search patients for Doctor ID {doctorId}.");
                throw;
            }
        }


        public async Task<SearchResultDto> GeneralSearchAsync(string filter, string keyword)
        {
            try
            {
                var result = new SearchResultDto();
                keyword = keyword?.Trim().ToLower() ?? string.Empty;

                // Validate filter
                if (string.IsNullOrEmpty(filter) || !new[] { "Doctor", "Clinic", "Specialization" }.Contains(filter))
                {
                    throw new ArgumentException("Invalid filter. Must be 'Doctor', 'Clinic', or 'Specialization'.");
                }

                if (filter == "Doctor")
                {
                    var doctors = await _unitOfWork.DoctorRepository
                        .GetQuery()
                        .Include(d => d.User)
                        .Include(d => d.Specialization)
                        .Include(d => d.Clinic)
                        .Where(d => string.IsNullOrEmpty(keyword) ||
                                    d.User.UserName.ToLower().Contains(keyword) ||
                                    d.User.Email.ToLower().Contains(keyword) ||
                                    d.Specialization.Name.ToLower().Contains(keyword) ||
                                    d.Clinic.Name.ToLower().Contains(keyword))
                        .Select(d => new DoctorSearchDto
                        {
                            UserId = d.UserId,
                            UserName = d.User.UserName,
                            Email = d.User.Email,
                            SpecializationName = d.Specialization.Name,
                            ClinicName = d.Clinic.Name
                        })
                        .ToListAsync();

                    result.Doctors = doctors;
                    result.Message = doctors.Any() ? "Doctors found successfully." : "No doctors found.";
                }
                else if (filter == "Clinic")
                {
                    var clinics = await _unitOfWork.ClinicRepository
                        .GetQuery()
                        .Where(c => string.IsNullOrEmpty(keyword) ||
                                    c.Name.ToLower().Contains(keyword) ||
                                    c.Address.ToLower().Contains(keyword))
                        .Select(c => new ClinicSearchDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Address = c.Address
                        })
                        .ToListAsync();

                    result.Clinics = clinics;
                    result.Message = clinics.Any() ? "Clinics found successfully." : "No clinics found.";
                }
                else if (filter == "Specialization")
                {
                    var specializations = await _unitOfWork.SpecializationRepository
                        .GetQuery()
                        .Where(s => string.IsNullOrEmpty(keyword) ||
                                    s.Name.ToLower().Contains(keyword) ||
                                    s.Description.ToLower().Contains(keyword))
                        .Select(s => new SpecializationSearchDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Description = s.Description
                        })
                        .ToListAsync();

                    result.Specializations = specializations;
                    result.Message = specializations.Any() ? "Specializations found successfully." : "No specializations found.";
                }

                return result;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid search filter provided.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during general search.");
                throw;
            }
        }

        public async Task<SearchResultDto> SearchBySpecializationAsync(string keyword)
        {
            try
            {
                var result = new SearchResultDto();
                keyword = keyword?.Trim().ToLower() ?? string.Empty;

                var specializations = await _unitOfWork.SpecializationRepository
                    .GetQuery()
                    .Where(s => string.IsNullOrEmpty(keyword) ||
                                s.Name.ToLower().Contains(keyword) ||
                                s.Description.ToLower().Contains(keyword))
                    .Select(s => new SpecializationSearchDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description
                    })
                    .ToListAsync();

                result.Specializations = specializations;
                result.Message = specializations.Any() ? "Specializations found successfully." : "No specializations found.";

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during search by specialization.");
                throw;
            }
        }


        

       
    }
}