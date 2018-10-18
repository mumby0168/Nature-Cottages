using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NatureCottages.Database.Repositorys.Core.Interfaces
{
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {
        Task AddAysnc(TEntity entity);
        Task AddRangeAysnc(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> FindAysnc(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAysnc();
        Task<TEntity> GetAsync(int id);
        Task RemoveAysnc(TEntity entity);
        Task RemoveRangeAysnc(IEnumerable<TEntity> entities);
        Task<TEntity> SingleOrDefaultAysnc(Expression<Func<TEntity, bool>> predicate);
    }
}