namespace LeagueUserSearchAPI.DTOs
{
    public class MiniSeriesDTO
    {
        public required int Losses { get; set; }
        public required string Progress { get; set; }
        public required int Target { get; set; }
        public required int Wins { get; set; }
    }
}