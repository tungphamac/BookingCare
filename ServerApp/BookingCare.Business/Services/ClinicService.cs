using BookingCare.Data.DTOs;
using BookingCare.Data.Models;
using BookingCare.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class ClinicService
    {
        private readonly ClinicRepository _clinicRepository;

        public ClinicService(ClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        // Lấy tất cả phòng khám với thông tin bác sĩ
        public async Task<IEnumerable<ClinicDoctorDto>> GetClinicsWithDoctorsAsync()
        {
            return await _clinicRepository.GetClinicsWithDoctorsAsync();
        }

        // Lấy phòng khám theo ID
        public async Task<Clinic?> GetClinicByIdAsync(int id)
        {
            return await _clinicRepository.GetClinicByIdAsync(id);
        }
        // Thêm phòng khám mới
        public async Task AddClinicAsync(Clinic clinic, List<int> doctorIds)
        {
            await _clinicRepository.AddClinicAsync(clinic, doctorIds);
        }

        // Cập nhật phòng khám
        public async Task UpdateClinicAsync(Clinic clinic, List<int> doctorIds)
        {
            await _clinicRepository.UpdateClinicAsync(clinic, doctorIds);
        }
        // Xóa phòng khám
        public async Task DeleteClinicAsync(int id)
        {
            await _clinicRepository.DeleteClinicAsync(id);
        }
    }


}
