using Microsoft.EntityFrameworkCore;

namespace SpreadMagic.Data.Sql.Support
{
    public class SqlConfigurer : IConfigurer
    {
        private readonly string _connectionString;

        public SqlConfigurer(string connectionString) => _connectionString = connectionString;

        public void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}