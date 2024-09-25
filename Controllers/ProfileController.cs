using Microsoft.AspNetCore.Mvc;
using LeagueUserSearchAPI.Services;
using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IRiotApiService _riotApiService;

        public ProfileController(IRiotApiService riotApiService)
        {
            _riotApiService = riotApiService;
        }

        // GET: api/profile/{puuid}
        [HttpGet("{puuid}")]
        public async Task<IActionResult> GetProfile(string puuid) 
        {
            if (string.IsNullOrEmpty(puuid))
            {
                return BadRequest("Puuid is required");
            }

            try
            {
                var profileData = await _riotApiService.GetProfileByPuuidAsync(puuid);

                if (profileData == null)
                {
                    return NotFound("Profile not found");
                }

                return Ok(profileData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error: {ex.Message}");
            }
        }

    }
}