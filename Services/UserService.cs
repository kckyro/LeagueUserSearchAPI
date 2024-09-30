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

        public async Task<User?> SearchUser(string gameName, string tagLine)
        {
            if (string.IsNullOrWhiteSpace(gameName) || string.IsNullOrWhiteSpace(tagLine))
            {
                return null;
            }

            try
            {   
                var accountResponse = await _riotApiService.GetRiotId(gameName, tagLine);
                if (accountResponse != null && !string.IsNullOrEmpty(accountResponse.Puuid))
                {
                    var summonerResponse = await _riotApiService.GetSummoner(accountResponse.Puuid);
                    if (summonerResponse != null) {
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
                    return null;
                }
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}