using System.Linq.Expressions;

namespace Auth.Sqlite.Repositories.Base
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 1/23/2023 2:34:20 PM
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);

        void Delete(object id);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(object id);

        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        public void Save();
    }
}
