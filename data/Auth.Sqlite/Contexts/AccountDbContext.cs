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

            string dbPath = $@"{GetSolutionDirectory()}\data\Auth.Sqlite\Accounts.sqlite";
            optionsBuilder.UseSqlite($"Data Source={dbPath};Mode=ReadWriteCreate;");
        }

        /// <summary>
        /// Get the path where the solution file exists (*.sln)
        /// </summary>
        static string GetSolutionDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            bool solutionFound = Directory.GetFiles(currentDirectory, "*.sln").Length > 0;

            // Traverse up until solution file is found
            while (!solutionFound)
            {
                DirectoryInfo parentDirectory = Directory.GetParent(currentDirectory);
                if (parentDirectory == null) // reached root directory
                    throw new Exception("Solution file not found.");

                solutionFound = Directory.GetFiles(parentDirectory.FullName, "*.sln").Length > 0;
                currentDirectory = parentDirectory.FullName;
            }

            return currentDirectory;
        }
    }
}
