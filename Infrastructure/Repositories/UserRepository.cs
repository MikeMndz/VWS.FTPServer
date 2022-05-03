using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User, MySqlConnection>, IUserRepository
    {
        public UserRepository() : base(ConfigurationHandler.GetConnectionString()) { }

        public User GetById(int id, string cacheKey = null, IDbTransaction transaction = null) =>
            Query(id, cacheKey: cacheKey, transaction: transaction).FirstOrDefault();

        public User FirstOrDefault(string cacheKey = null, IDbTransaction transaction = null) =>
            QueryAll(cacheKey: cacheKey, transaction: transaction).FirstOrDefault();

        public User FirstOrDefault(Expression<Func<User, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null) =>
            Query(predicate, cacheKey: cacheKey, transaction: transaction).FirstOrDefault();

        public ulong Add(User entity, IDbTransaction transaction = null) =>
            (ulong)Insert(entity, transaction: transaction);

        public void AddRange(IEnumerable<User> entities, IDbTransaction transaction = null) =>
            InsertAll(entities, transaction: transaction);

        public void Edit(User entity, IDbTransaction transaction = null)
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


        public void EditRange(IEnumerable<User> entities, IDbTransaction transaction = null) =>
            UpdateAll(entities, transaction: transaction);

        public void Remove(User entity, IDbTransaction transaction = null) =>
            Delete(entity, transaction: transaction);

        public void RemoveRange(IEnumerable<User> entities, IDbTransaction transaction = null) =>
            DeleteAll(entities, transaction: transaction);

        public IEnumerable<User> GetAll(string cacheKey = null, IDbTransaction transaction = null) =>
            QueryAll(transaction: transaction);

        public IEnumerable<User> GetWhere(Expression<Func<User, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null) =>
            Query(predicate, transaction: transaction);

        public long CountAll(IDbTransaction transaction = null) =>
            CountAll(transaction: transaction);

        public long CountWhere(Expression<Func<User, bool>> predicate, IDbTransaction transaction = null) =>
            Count(predicate, transaction: transaction);
    }
}
