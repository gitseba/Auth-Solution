using Auth.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Auth.Sqlite.Contexts
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext()
        {

        }

        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {
            this.SaveChangesFailed += (args, err) =>
            {
                Debug.WriteLine("Saving model failed because: " + err.Exception.Message);
            };
        }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string dbPath = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).FullName}\Auth.Sqlite\Accounts.sqlite";
            optionsBuilder.UseSqlite($"Data Source={dbPath};Mode=ReadWriteCreate;");
        }
    }
}
