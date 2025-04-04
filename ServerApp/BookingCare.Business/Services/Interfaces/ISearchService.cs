using BookingCare.API.Dtos;

namespace BookingCare.Business.Services.Interfaces

{
    public interface ISearchService
    {
        Task<SearchResultDto> GeneralSearchAsync(string filter, string keyword);
        Task<SearchResultDto> SearchBySpecializationAsync(string keyword);
        Task<SearchResultDto> SearchPatientsForDoctorAsync(int doctorId, string keyword);
    }
}