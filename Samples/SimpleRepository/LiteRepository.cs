using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LiteDB;

namespace SimpleRepository
{
    public class LiteRepository : IRepository
    {
        private readonly string _connectionString;

        public LiteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> Get<T>()
        {
            return await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                return db.Query<T>().ToList();
            });
        }

        public async Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(()=>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                return db.Query<T>().Where(predicate).ToList();
            });
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> predicate, bool isNullable)
        {
            
            if (isNullable)
            {
                return await Task.Run(() =>
                {
                    using var db = new LiteDB.LiteRepository(_connectionString);
                    return db.Query<T>().Where(predicate).FirstOrDefault();
                });
            }

            return await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                return db.Query<T>().Where(predicate).Single();
            });
        }

        public async Task<T> Get<T>(Guid Id, bool isNullable = true) where T : IEntity
        {
            return await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                if (isNullable)
                {
                    return db.Query<T>().Where(e => e.Id == Id).FirstOrDefault();
                }

                return db.Query<T>().Where(e => e.Id == Id).Single();
            });
        }

        public async Task Post<T>(T entity)
        {
            await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                db.Insert(entity);
            });
        }

        public async Task Put<T>(T entity)
        {
            await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                db.Update(entity);
            });
        }

        public async Task Delete<T>(Guid id) where T : IEntity
        {
            await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                db.Delete<T>(e => e.Id == id);
            });
        }


    }
}
