using Microsoft.EntityFrameworkCore;
using FCF.Test.Helpers;


namespace FCF.Test.Mocks
{
    public class MockContext<TContext> : IDisposable where TContext : DbContext
    {
        private SqlServerDbManager<TContext> SqliteMemoryDbManager { get; }

        public TContext Context { get; }

        public MockContext(Func<DbContextOptions<TContext>, TContext> creator)
        {
            SqliteMemoryDbManager = new SqlServerDbManager<TContext>();
            Context = creator(SqliteMemoryDbManager.Options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Dispose();
            SqliteMemoryDbManager.Dispose();
        }
    }
}
