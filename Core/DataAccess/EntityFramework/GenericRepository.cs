using Core.DataAccess.EntityFramework;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.GenericRepository
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : BaseEntity, new()
        where TContext : DbContext
    {
        private readonly TContext dbContext;
        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

        public GenericRepository(TContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return dbContext.SaveChanges();
        }

        public int Add(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
            
        }

        public Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int AddOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddOrUpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task BulkAdd(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbContext.Set<TEntity>().Where(predicate);

            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public virtual async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            if (noTracking)
                return await entity.AsNoTracking().ToListAsync();
            return await entity.ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {

            dbContext.Set<TEntity>().Update(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
           return await dbContext.Set<TEntity>().SingleOrDefaultAsync(filter);
        }
    }
}
