using System;
using System.Threading.Tasks;

namespace SpreadMagic.Core
{
    public interface IGameService
    {
        Task<Game[]> GetFutureGamesAsync();
        Task<GameDetails[]> GetAllGamesAsync(DetailsFilter detailFilter);
    }
}