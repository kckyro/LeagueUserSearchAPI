using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.DTOs
{
    public class ProfileDTO
    {
        public string? GameName { get; set; }
        public string? TagLine { get; set; }
        public string? Region { get; set; }
        public required int ProfileIconId { get; set; }

    }
}

