using SpreadMagic.Data.Sql.Support;
using ParentGameContext = SpreadMagic.Data.Contexts.GameContext;

namespace SpreadMagic.Data.Sql.Contexts
{
    public class GameContext : ParentGameContext
    {
        public GameContext(string connectionString) : base(new SqlConfigurer(connectionString))
        {
            
        }
    }
}
