using System;

namespace SpreadMagic.Core
{
    public class Game
    {
        public int Id { get; }

        public int HomeTeamId { get; }

        public int AwayTeamId { get; }

        public DateTime DateAndTime { get; }

        public Game(int id, int homeTeamId, int awayTeamId, DateTime dateAndTime)
        {
            Id = id;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            DateAndTime = dateAndTime;
        }
    }
}