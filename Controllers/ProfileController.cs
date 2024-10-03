using Microsoft.AspNetCore.Mvc;
using LeagueUserSearchAPI.Services;

namespace LeagueUserSearchAPI.Controllers
{
    [ApiController]
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{gameName}/{tagLine}")]
        public async Task<IActionResult> GetProfile(string gameName, string tagLine) 
        {
            if (string.IsNullOrEmpty(gameName) || string.IsNullOrEmpty(tagLine))
            {
                return BadRequest("gameName and tagLine is required");
            }

            try
            {
                var profileData = await _profileService.DisplayProfile(gameName, tagLine);

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