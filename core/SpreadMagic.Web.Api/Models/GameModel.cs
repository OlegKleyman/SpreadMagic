using System;

namespace SpreadMagic.Web.Api.Models
{
    public class GameModel
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public DateTime DateAndTime { get; set; }
        public decimal Spread { get; internal set; }
    }
}