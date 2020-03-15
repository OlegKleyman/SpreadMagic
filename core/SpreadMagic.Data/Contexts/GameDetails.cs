using System;

namespace SpreadMagic.Data.Contexts
{
    public class GameDetails
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime DateAndTime { get; set; }
        public decimal Spread { get; set; }
        public decimal? ModelPrediction { get; set; }
        public int? HomeScore { get; set; }
        public int? VisitorScore { get; set; }
    }
}