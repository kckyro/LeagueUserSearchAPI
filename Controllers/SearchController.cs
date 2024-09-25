using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LeagueUserSearchAPI.Models;
using LeagueUserSearchAPI.Services;

namespace LeagueUserSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SearchController : ControllerBase
    {
        private readonly UserService _userService;

        public SearchController(UserService userService) 
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Search(string gameName, string tagLine)
        {
            if (string.IsNullOrWhiteSpace(gameName) || string.IsNullOrWhiteSpace(tagLine))
            {
                return BadRequest("Both gameName and tagLine are required.");
            }

            var result = await _userService.SearchUser(gameName, tagLine);
            
            if (result == null)
            {
                return NotFound("User not found.");
            }

            return Ok(result);   
        }
    }

    

}