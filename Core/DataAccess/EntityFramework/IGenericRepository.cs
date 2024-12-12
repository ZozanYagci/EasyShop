using AutoMapper.Execution;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    //generic constraint - generic kısıt
    public interface IGenericRepository<T> where T: BaseEntity, new() // BaseEntity zaten bir class olduğu için kısıtlamalara tekrardan -class- eklemedim.
    {
        Task<int> AddAsync(T entity);
        int Add(T entity);
        int Add(IEnumerable<T> entities);
        Task<int> AddAsync(IEnumerable<T> entities);


        Task<int> UpdateAsync(T entity);
        int Update(T entity);



        Task<int> DeleteAsync(T entity);
        int Delete(T entity);
        Task<int> DeleteAsync(Guid id);
        int Delete(Guid id);
        bool DeleteRange(Expression<Func<T, bool>> predicate);
        Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate);


        Task<int> AddOrUpdateAsync(T entity);

        int AddOrUpdate(T entity);

        IQueryable<T> AsQueryable();

        Task<List<T>> GetAll(bool noTracking = true);

        Task<List<T>> GetList(Expression<Func<T, bool>> predicate, bool noTracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);


        Task BulkDeleteById(IEnumerable<Guid> ids);
        Task BulkDelete(Expression<Func<T, bool>> predicate);
        Task BulkDelete(IEnumerable<T> entities);
        Task BulkUpdate(IEnumerable<T> entities);
        Task BulkAdd(IEnumerable<T> entities);
    }
}
