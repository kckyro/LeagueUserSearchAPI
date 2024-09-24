using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LeagueUserSearchAPI.Models;
using LeagueUserSearchAPI.Services;

namespace LeagueUserSearchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly UserService? _userService;

        public SearchController() 
        {
            _userService = new UserService();
        }
        
        // GET api/search?query=[STRING]
        [HttpGet]
        public IActionResult Search([FromQuery] string query)
        {
            var matchingUsers = _userService.SearchUsers(query);
            return Ok(matchingUsers);     
        }
    }

    

}