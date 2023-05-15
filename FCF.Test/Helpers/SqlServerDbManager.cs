using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FCF.Test.Helpers
{
    public class SqlServerDbManager<TContext> : IDisposable where TContext : DbContext
    {
        public DbContextOptions<TContext> Options { get; }

        private readonly SqliteConnection Connection;

        public SqlServerDbManager()
        {
            Options = new DbContextOptionsBuilder<TContext>().UseSqlServer("Server=AHMEDNAEEM-LT\\MSSQLSERVER01;Database=FCF_V6;User id=FOLIO3\\ahmednaeem;password=9026040An!;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True").Options;
        }

        public void Dispose()
        {
           
        }
    }
}