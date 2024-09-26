using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.Services
{
    public class ProfileService
    {
        private readonly IRiotApiService _riotApiService;

        public ProfileService(IRiotApiService riotApiService)
        {
            _riotApiService = riotApiService;
        }

        public async Task<User> DisplayUser(string Puuid)
        {
            if (string.IsNullOrWhiteSpace(Puuid))
            {
                return null;
            }

            try
            {   
                var summonerResponse = await _riotApiService.GetProfileByPuuidAsync(Puuid);

                return new User
                {
                    Id = summonerResponse.Id,
                    Puuid = summonerResponse.Puuid,
                    GameName = summonerResponse.Name,
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