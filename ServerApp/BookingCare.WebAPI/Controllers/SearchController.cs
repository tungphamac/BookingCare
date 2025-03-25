using BookingCare.API.Dtos;
using BookingCare.Business.Services;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // GET: api/Search?filter=Doctor&keyword=cardiology
        [HttpGet]
        public async Task<ActionResult<SearchResultDto>> GeneralSearch([FromQuery] string filter, [FromQuery] string keyword)
        {
            try
            {
                var result = await _searchService.GeneralSearchAsync(filter, keyword);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        // GET: api/Search/Specialization?keyword=cardiology
        [HttpGet("Specialization")]
        public async Task<ActionResult<SearchResultDto>> SearchBySpecialization([FromQuery] string keyword)
        {
            try
            {
                var result = await _searchService.SearchBySpecializationAsync(keyword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}