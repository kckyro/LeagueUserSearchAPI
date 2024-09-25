using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.Services
{
    public class UserService
    {
        private readonly IRiotApiService _riotApiService;

        public UserService(IRiotApiService riotApiService)
        {
            _riotApiService = riotApiService;
        }

        public async Task<User> SearchUser(string gameName, string tagLine)
        {
            if (string.IsNullOrWhiteSpace(gameName) || string.IsNullOrWhiteSpace(tagLine))
            {
                return null;
            }

            try
            {   
                var accountResponse = await _riotApiService.GetUserPuuidAsync(gameName, tagLine);
                var summonerResponse = await _riotApiService.GetProfileByPuuidAsync(accountResponse.Puuid);

                return new User
                {
                    Id = summonerResponse.Id,
                    Puuid = summonerResponse.Puuid,
                    GameName = accountResponse.GameName,
                    TagLine = accountResponse.TagLine,
                    ProfileIconId = summonerResponse.ProfileIconId,
                    SummonerLevel = summonerResponse.SummonerLevel
                };
            }
            catch (HttpRequestException)
            {
                // The user was not found or there was an API error
                return null;
            }
        }
    }
}