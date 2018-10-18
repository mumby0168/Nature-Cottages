using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Persitance;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.Core
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly CottageDbContext Context;

        public RepositoryAsync(CottageDbContext context)
        {
            Context = context;
        }
        public async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAysnc()
        {
            return await Task.Run(() => Context.Set<TEntity>().ToList());
        }

        public async Task<IEnumerable<TEntity>> FindAysnc(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => Context.Set<TEntity>().Where(predicate));
        }

        public async Task<TEntity> SingleOrDefaultAysnc(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => Context.Set<TEntity>().FirstOrDefault(predicate));
        }

        public async Task AddAysnc(TEntity entity)
        {
            await Task.Run(() => Context.Set<TEntity>().Add(entity));
        }

        public async Task AddRangeAysnc(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => Context.Set<TEntity>().AddRange(entities));
        }

        public async Task RemoveAysnc(TEntity entity)
        {
            await Task.Run(() => Context.Set<TEntity>().Remove(entity));
        }

        public async Task RemoveRangeAysnc(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => Context.Set<TEntity>().RemoveRange(entities));
        }
    }
}
