using LeagueUserSearchAPI.DTOs;
using System.Text.Json;

namespace LeagueUserSearchAPI.Services
{
    public class ProfileService
    {
        private readonly IRiotApiService _riotApiService;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(IRiotApiService riotApiService, ILogger<ProfileService> logger)
        {
            _riotApiService = riotApiService;
            _logger = logger;
        }

        public async Task<ProfileDTO> DisplayUser(string Puuid)
        {
            if (string.IsNullOrWhiteSpace(Puuid))
            {
                return null;
            }

            try
            {   
                var summonerResponse = await _riotApiService.GetSummoner(Puuid);

                _logger.LogInformation($"summonerResponse: {JsonSerializer.Serialize(summonerResponse)}");

                return new ProfileDTO
                {
                    GameName = null,
                    GameTag = null,
                    Region = null,
                    
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