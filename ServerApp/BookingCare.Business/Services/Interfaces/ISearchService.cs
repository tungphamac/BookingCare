using BookingCare.API.Dtos;

namespace BookingCare.Business.Services.Interfaces

{
    public interface ISearchService
    {
        Task<SearchResultDto> GeneralSearchAsync(string filter, string keyword);
        Task<SearchResultDto> SearchBySpecializationAsync(string keyword);
    }
}