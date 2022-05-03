using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        #region Sync

        T GetById(int id, string cacheKey = null, IDbTransaction transaction = null);
        T FirstOrDefault(string cacheKey = null, IDbTransaction transaction = null);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null);
        ulong Add(T entity, IDbTransaction transaction = null);
        void AddRange(IEnumerable<T> entities, IDbTransaction transaction = null);
        void Edit(T entity, IDbTransaction transaction = null);
        void EditRange(IEnumerable<T> entities, IDbTransaction transaction = null);
        void Remove(T entity, IDbTransaction transaction = null);
        void RemoveRange(IEnumerable<T> entities, IDbTransaction transaction = null);
        IEnumerable<T> GetAll(string cacheKey = null, IDbTransaction transaction = null);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null);
        long GetCountAll(IDbTransaction transaction = null);
        long GetCountWhere(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);

        #endregion

        #region Async

        Task<T> GetByIdAsync(int id, string cacheKey = null, IDbTransaction transaction = null);
        Task<T> FirstOrDefaultAsync(string cacheKey = null, IDbTransaction transaction = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null);
        Task<ulong> AddAsync(T entity, IDbTransaction transaction = null);
        Task<int> AddRangeAsync(IEnumerable<T> entities, IDbTransaction transaction = null);
        Task<int> EditAsync(T entity, IDbTransaction transaction = null);
        Task<int> EditRangeAsync(IEnumerable<T> entities, IDbTransaction transaction = null);
        Task<int> RemoveAsync(T entity, IDbTransaction transaction = null);
        Task<int> RemoveRangeAsync(IEnumerable<T> entities, IDbTransaction transaction = null);
        Task<IEnumerable<T>> GetAllAsync(string cacheKey = null, IDbTransaction transaction = null);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null);
        Task<long> GetCountAllAsync(IDbTransaction transaction = null);
        Task<long> GetCountWhereAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);

        #endregion
    }
}
