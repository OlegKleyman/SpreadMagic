using System;
using Microsoft.EntityFrameworkCore;
using SpreadMagic.Data.Entities;

namespace SpreadMagic.Data.Contexts
{
    public class GameContext : DbContext
    {
        private readonly IConfigurer _configurer;

        public GameContext(IConfigurer configurer) => _configurer = configurer;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            _configurer.Configure(optionsBuilder);
        }

        private DbSet<Game>? _games;

        public DbSet<Game> Games
        {
            get => _games ?? throw new InvalidOperationException($"{nameof(Games)} is null.");
            set => _games = value;
        }

        private DbSet<GameDetails>? _gamesDetails;

        public DbSet<GameDetails> GameDetails
        {
            get => _gamesDetails ?? throw new InvalidOperationException($"{nameof(GameDetails)} is null.");
            set => _gamesDetails = value;
        }
    }
}