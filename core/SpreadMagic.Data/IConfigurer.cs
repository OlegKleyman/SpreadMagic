using Microsoft.EntityFrameworkCore;

namespace SpreadMagic.Data
{
    public interface IConfigurer
    {
        void Configure(DbContextOptionsBuilder optionsBuilder);
    }
}