using System.Threading.Tasks;
using LeagueUserSearchAPI.DTOs;

public interface IRiotApiService
{
    Task<SummonerDTO?> GetSummoner(string puuid);
    Task<AccountDTO?> GetRiotId(string userName, string tagLine);
}