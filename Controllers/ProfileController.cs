using Microsoft.AspNetCore.Mvc;
using LeagueUserSearchAPI.Services;
using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
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
                var profileData = await _profileService.DisplayUser(puuid);

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