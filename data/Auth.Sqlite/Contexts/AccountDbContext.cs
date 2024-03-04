using Auth.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;

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

        }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string dbPath = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).FullName}\Auth.Sqlite\Accounts.sqlite";
            optionsBuilder.UseSqlite($"Data Source={dbPath};Mode=ReadWriteCreate;");
        }
    }
}
