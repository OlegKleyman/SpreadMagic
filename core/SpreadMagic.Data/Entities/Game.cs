using System;
using System.Collections.ObjectModel;

namespace SpreadMagic.Data.Entities
{
    public class Game
    {
        public DateTime DateAndTime { get; set; }

        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }
        public decimal Spread { get; set; }
    }
}