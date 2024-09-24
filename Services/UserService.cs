using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.Services
{
    public class UserService
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "Summoner1" },
            new User { Id = 2, Username = "Summoner2" },
            new User { Id = 3, Username = "LeagueMaster" },
            new User { Id = 4, Username = "Wully" },
            new User { Id = 5, Username = "strawberry54" },
            new User { Id = 5, Username = "HATE LEAGUE" }
        };

        public List<User> SearchUsers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<User>();
            }

             return users
                .Where(user => user.Username.Contains(query, System.StringComparison.OrdinalIgnoreCase))
                .Select(user => new User
                {
                    Id = user.Id,
                    Username = user.Username
                })
                .ToList();
        }
    }
}