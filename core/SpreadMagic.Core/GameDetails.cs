using System;

namespace SpreadMagic.Core
{
    public class GameDetails
    {
        public GameDetails(int id, int homeTeamId, int awayTeamId, DateTime dateAndTime, decimal spread, decimal? modelPrediction, int? homeScore, int? visitorScore)
        {
            Id = id;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            DateAndTime = dateAndTime;
            Spread = spread;
            ModelPrediction = modelPrediction;
            HomeScore = homeScore;
            VisitorScore = visitorScore;            
        }

        public int Id { get; }
        public int HomeTeamId { get; }
        public int AwayTeamId { get; }
        public DateTime DateAndTime { get; }
        public decimal Spread { get; }
        public decimal? ModelPrediction { get; }
        public int? HomeScore { get; }
        public int? VisitorScore { get; }
    }
}