namespace LeagueUserSearchAPI.DTOs
{
    public class LeagueEntryDTO
    {
        public required string LeagueId { get; set; }
        public required string SummonerId { get; set; }
        public required string QueueType { get; set; }
        public required string Tier { get; set; }
        public required string Rank { get; set; }
        public required int LeaguePoints { get; set; }
        public required int Wins { get; set; }
        public required int Losses { get; set; }
        public required bool HotStreak { get; set; }
        public required bool Veteran { get; set; }
        public required bool FreshBlood { get; set; }
        public required bool Inactive { get; set; }
        public required MiniSeriesDTO MiniSeries { get; set; }
    }
}
