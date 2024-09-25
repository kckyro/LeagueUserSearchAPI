using LeagueUserSearchAPI.Models;
using LeagueUserSearchAPI.DTOs;

namespace LeagueUserSearchAPI.Services
{
    public class RiotApiService : IRiotApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _region;
        

        public RiotApiService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = configuration["RiotApi:ApiKey"];
            _region = configuration["RiotApi:Region"];
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _apiKey);
            
        }

        public async Task<AccountDto> GetUserPuuidAsync(string gameName, string tagLine)
        {
            try 
            {
                if (string.IsNullOrWhiteSpace(gameName) || string.IsNullOrWhiteSpace(tagLine))
                {
                    return null;
                }

                var response = await _httpClient.GetFromJsonAsync<AccountDto>(
                        $"https://americas.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{Uri.EscapeDataString(gameName)}/{Uri.EscapeDataString(tagLine)}");

                if (response == null)
                {
                    return null;
                }

                return new AccountDto
                {
                    Puuid = response.Puuid,
                    GameName = response.GameName,
                    TagLine = response.TagLine                  
                };
            }
            catch (HttpRequestException)
            {
                return null;
            }           
        }

        public async Task<ProfileDto> GetProfileByPuuidAsync(string puuid)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ProfileDto>(
                        $"https://{_region}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}");

                if (response == null)
                {
                    return null;
                }

                return new ProfileDto
                {
                    Id = response.Id,
                    Puuid = response.Puuid,
                    Name = response.Name,
                    ProfileIconId = response.ProfileIconId,
                    SummonerLevel = response.SummonerLevel
                };
            } 
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}