using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class ClinicService : BaseService<Clinic>, IClinicService
    {
        public ClinicService(ILogger<ClinicService> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
        }

        public async Task<ClinicDetailDto?> GetClinicDetailAsync(int id)
        {
            try
            {
                var clinic = await _unitOfWork.ClinicRepository
                    .GetQuery(c => c.Id == id)
                    .Select(c => new ClinicDetailDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Address = c.Address,
                        Phone = c.Phone,
                        Introduction = c.Introduction,
                        CreateAt = c.CreateAt
                    })
                    .FirstOrDefaultAsync();

                if (clinic == null)
                {
                    _logger.LogWarning($"Clinic with ID {id} not found.");
                    return null;
                }

                return clinic;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving clinic details for ID {id}.");
                throw;
            }
        }
    }
}