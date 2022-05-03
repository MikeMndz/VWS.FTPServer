using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : BaseRepository<T, MySqlConnection>, IGenericRepository<T> where T : class
    {
        #region Constructors

        public GenericRepository() : 
            base(
            connectionString: ConfigurationHandler.GetConnectionString()
            ) 
        { }

        #endregion

        #region Sync Methods

        public T GetById(int id, string cacheKey = null, IDbTransaction transaction = null) => 
            Query(id, cacheKey: cacheKey, transaction: transaction).FirstOrDefault();

        public T FirstOrDefault(string cacheKey = null, IDbTransaction transaction = null) =>
            QueryAll(cacheKey: cacheKey, transaction: transaction).FirstOrDefault();

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null) => 
            Query(predicate, cacheKey: cacheKey, transaction: transaction).FirstOrDefault();

        public ulong Add(T entity, IDbTransaction transaction = null) => 
            (ulong)Insert(entity, transaction: transaction);

        public void AddRange(IEnumerable<T> entities, IDbTransaction transaction = null) =>
            InsertAll(entities, transaction: transaction);

        public void Edit(T entity, IDbTransaction transaction = null)
        {
            try
            {
                Update(entity, transaction: transaction);
            }
            catch (Exception e)
            {
                throw;
            }
        }
            
        public void EditRange(IEnumerable<T> entities, IDbTransaction transaction = null) =>
            UpdateAll(entities, transaction: transaction);

        public void Remove(T entity, IDbTransaction transaction = null) =>
            Delete(entity, transaction: transaction);

        public void RemoveRange(IEnumerable<T> entities, IDbTransaction transaction = null) => 
            DeleteAll(entities, transaction: transaction);

        public IEnumerable<T> GetAll(string cacheKey = null, IDbTransaction transaction = null) =>
            QueryAll(transaction: transaction);

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null) =>
            Query(predicate, transaction: transaction);

        public long GetCountAll(IDbTransaction transaction = null) =>
            CountAll(transaction: transaction);

        public long GetCountWhere(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) => 
            Count(predicate, transaction: transaction);

        #endregion

        #region Async Methods

        public async Task<T> GetByIdAsync(int id, string cacheKey = null, IDbTransaction transaction = null) =>
            (await QueryAsync(id, cacheKey: cacheKey, transaction: transaction)).FirstOrDefault();

        public async Task<T> FirstOrDefaultAsync(string cacheKey = null, IDbTransaction transaction = null) =>
            (await QueryAllAsync(cacheKey: cacheKey, transaction: transaction)).FirstOrDefault();

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null) =>
            (await QueryAsync(predicate, cacheKey: cacheKey, transaction: transaction)).FirstOrDefault();

        public async Task<ulong> AddAsync(T entity, IDbTransaction transaction = null) =>
            (ulong)(await InsertAsync(entity, transaction: transaction));

        public async Task<int> AddRangeAsync(IEnumerable<T> entities, IDbTransaction transaction = null) =>
            await InsertAllAsync(entities, transaction: transaction);

        public async Task<int> EditAsync(T entity, IDbTransaction transaction = null) =>
            await UpdateAsync(entity, transaction: transaction);

        public async Task<int> EditRangeAsync(IEnumerable<T> entities, IDbTransaction transaction = null) =>
            await UpdateAllAsync(entities, transaction: transaction);

        public async Task<int> RemoveAsync(T entity, IDbTransaction transaction = null) =>
            await DeleteAsync(entity, transaction: transaction);

        public async Task<int> RemoveRangeAsync(IEnumerable<T> entities, IDbTransaction transaction = null) =>
            await DeleteAllAsync(entities, transaction: transaction);

        public async Task<IEnumerable<T>> GetAllAsync(string cacheKey = null, IDbTransaction transaction = null) =>
            await QueryAllAsync(cacheKey: cacheKey, transaction: transaction);

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null) =>
            await QueryAsync(predicate, cacheKey: cacheKey, transaction: transaction);

        public async Task<long> GetCountAllAsync(IDbTransaction transaction = null) => 
            await CountAllAsync(transaction: transaction);

        public async Task<long> GetCountWhereAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null) =>
            await CountAsync(predicate, transaction: transaction);

        #endregion
    }
}
