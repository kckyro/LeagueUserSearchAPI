using LeagueUserSearchAPI.Models;

namespace LeagueUserSearchAPI.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _region;

        public UserService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = configuration["RiotApi:ApiKey"];
            _region = configuration["RiotApi:Region"];
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _apiKey);
        }

        public async Task<User> SearchUser(string gameName, string tagLine)
        {
            if (string.IsNullOrWhiteSpace(gameName) || string.IsNullOrWhiteSpace(tagLine))
            {
                return null;
            }

            try
            {
                // Step 1: Get PUUID from Riot ID
                var accountResponse = await _httpClient.GetFromJsonAsync<AccountDto>(
                    $"https://americas.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{Uri.EscapeDataString(gameName)}/{Uri.EscapeDataString(tagLine)}");

                if (accountResponse == null)
                {
                    return null;
                }

                // Step 2: Get Summoner data using PUUID
                var summonerResponse = await _httpClient.GetFromJsonAsync<SummonerDto>(
                    $"https://{_region}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{accountResponse.Puuid}");

                if (summonerResponse == null)
                {
                    return null;
                }

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

    public class AccountDto
    {
        public string Puuid { get; set; }
        public string GameName { get; set; }
        public string TagLine { get; set; }
    }

    public class SummonerDto
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Puuid { get; set; }
        public string Name { get; set; }
        public int ProfileIconId { get; set; }
        public long RevisionDate { get; set; }
        public long SummonerLevel { get; set; }
    }
}