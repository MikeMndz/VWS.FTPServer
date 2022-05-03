using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id, string cacheKey = null, IDbTransaction transaction = null);
        User FirstOrDefault(string cacheKey = null, IDbTransaction transaction = null);
        User FirstOrDefault(Expression<Func<User, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null);
        ulong Add(User entity, IDbTransaction transaction = null);
        void AddRange(IEnumerable<User> entities, IDbTransaction transaction = null);
        void Edit(User entity, IDbTransaction transaction = null);
        void EditRange(IEnumerable<User> entities, IDbTransaction transaction = null);
        void Remove(User entity, IDbTransaction transaction = null);
        void RemoveRange(IEnumerable<User> entities, IDbTransaction transaction = null);
        IEnumerable<User> GetAll(string cacheKey = null, IDbTransaction transaction = null);
        IEnumerable<User> GetWhere(Expression<Func<User, bool>> predicate, string cacheKey = null, IDbTransaction transaction = null);
        long CountAll(IDbTransaction transaction = null);
        long CountWhere(Expression<Func<User, bool>> predicate, IDbTransaction transaction = null);
    }
}
