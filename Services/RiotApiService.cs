using System.Text.Json;
using LeagueUserSearchAPI.DTOs;

namespace LeagueUserSearchAPI.Services
{
    public class RiotApiService : IRiotApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _region;
        private readonly ILogger<ProfileService> _logger;
        

        public RiotApiService(IConfiguration configuration, HttpClient httpClient, ILogger<ProfileService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["RiotApi:ApiKey"] ?? throw new ArgumentNullException("RiotApi:ApiKey", "API key is missing from configuration.");;
            _region = configuration["RiotApi:Region"] ?? throw new ArgumentNullException("RiotApi:Region", "Region is missing from configuration.");;
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _apiKey);
            _logger = logger;
        }

        public async Task<AccountDTO?> GetRiotId(string gameName, string tagLine)
        {
            try {
                var response = await _httpClient.GetFromJsonAsync<AccountDTO>(
                    $"https://americas.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}"
                );

                if (response == null) { return null; }

                return new AccountDTO
                {
                    Puuid = response.Puuid,
                    GameName = response.GameName,
                    TagLine = response.TagLine,
                    SummonerLevel = response.SummonerLevel
                };
            } 
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<SummonerDTO?> GetSummoner(string puuid)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SummonerDTO>(
                        $"https://{_region}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}");

                if (response == null)
                {
                     _logger.LogWarning("GetSummoner Response: Riot API returned null for the provided gameName and tagLine.");
                    return null;
                }

                _logger.LogInformation($"GetSummoner Response: {JsonSerializer.Serialize(response)}");

                return new SummonerDTO
                {
                    AccountId = response.AccountId,
                    ProfileIconId = response.ProfileIconId,
                    RevisionDate = response.RevisionDate,
                    Id = response.Id,
                    Puuid = response.Puuid,
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