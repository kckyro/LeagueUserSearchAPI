namespace LeagueUserSearchAPI.Models
{
    public class User 
    {
        public string Id { get; set; }
        public string Puuid { get; set; }
        public string GameName { get; set; }
        public string TagLine { get; set; }
        public int ProfileIconId { get; set; }
        public long SummonerLevel { get; set; }
    }
}
