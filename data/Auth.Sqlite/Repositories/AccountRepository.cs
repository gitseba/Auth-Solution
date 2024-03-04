using Auth.Sqlite.Contexts;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories.Base;

namespace Auth.Sqlite.Repositories
{
    public class AccountRepository : BaseRepository<AccountEntity>
    {
        private readonly AccountDbContext _dbContext;

        public AccountRepository(AccountDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public override void Insert(AccountEntity entity)
        {
            base.Insert(entity);
            Save();
        }

        public override void Delete(object id)
        {
            base.Delete(id);
        }

        public override void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

