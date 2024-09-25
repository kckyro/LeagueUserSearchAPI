using System.Threading.Tasks;
using LeagueUserSearchAPI.DTOs;

public interface IRiotApiService
{
    Task<ProfileDto> GetProfileByPuuidAsync(string puuid);
    Task<AccountDto> GetUserPuuidAsync(string userName, string tagLine);
}