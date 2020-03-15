using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpreadMagic.Core;
using SpreadMagic.Data.Contexts;
using SpreadMagic.Data.Entities;
using GameEntity = SpreadMagic.Data.Entities.Game;

using SqlGameContext = SpreadMagic.Data.Sql.Contexts.GameContext;

namespace SpreadMagic.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>()
                    .AddScoped<IDateProvider, DateProvider>()
                    .AddScoped<GameContext>(provider =>
                        new SqlGameContext(Configuration.GetConnectionString("SpreadMagic")))
                    .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using var scope = app.ApplicationServices.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<GameContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Games.AddRange(new GameEntity
                    {
                        HomeTeamId = 1,
                        AwayTeamId = 2,
                        DateAndTime = DateTime.UtcNow.AddDays(4),
                        Spread = 3m
                    },
                    new GameEntity
                    {
                        HomeTeamId = 3,
                        AwayTeamId = 4,
                        DateAndTime = DateTime.UtcNow.AddDays(7),
                        Spread = -1.1m
                    },
                    new GameEntity
                    {
                        HomeTeamId = 3,
                        AwayTeamId = 4,
                        DateAndTime = DateTime.MinValue,
                        Spread = 3.2m
                    });

                context.SaveChanges();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
