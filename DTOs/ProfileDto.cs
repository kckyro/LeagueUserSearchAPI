using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.DTOs
{
    public class ProfileDTO
    {
        public string GameName { get; set; }
        public string GameTag { get; set; }
        public string Region { get; set; }
        public int ProfileIconId { get; set; }

    }
}

