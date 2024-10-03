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

        public async Task<ProfileDTO?> DisplayProfile(string gameName, string tagLine)
        {
            if (string.IsNullOrWhiteSpace(gameName) || string.IsNullOrWhiteSpace(tagLine))
            {
                return null;
            }

            try
            {   
                var riotResponse = await _riotApiService.GetRiotId(gameName, tagLine);

                if (riotResponse == null)
                {
                    _logger.LogWarning("DisplayProfile Response: Riot API returned null for the provided gameName and tagLine.");
                    return null;
                }

                _logger.LogInformation($"DisplayProfile Response: {JsonSerializer.Serialize(riotResponse)}");

                return new ProfileDTO
                {
                    GameName = riotResponse.GameName,
                    TagLine = riotResponse.TagLine,
                    Region = "na1",
                    ProfileIconId = 0
                    
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