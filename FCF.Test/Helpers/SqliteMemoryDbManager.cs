using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FCF.Test.Helpers
{
    public class SqliteMemoryDbManager<TContext> : IDisposable where TContext : DbContext
    {
        public DbContextOptions<TContext> Options { get; }

        private readonly SqliteConnection Connection;

        public SqliteMemoryDbManager()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            Options = new DbContextOptionsBuilder<TContext>().UseSqlite(Connection).Options;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}