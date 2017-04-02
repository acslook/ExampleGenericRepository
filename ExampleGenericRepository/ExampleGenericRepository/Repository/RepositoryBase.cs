using SQLite.Net.Attributes;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExampleGenericRepository.Interfaces;
using ExampleGenericRepository.Models;

namespace ExampleGenericRepository.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly SQLiteConnection Connection;

        public RepositoryBase(SQLiteConnection connection)
        {
            Connection = connection;
        }

        public void Add(TEntity obj)
        {
            Connection.Insert(obj);
        }
        
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.Table<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Connection.Table<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return Connection.Table<TEntity>().FirstOrDefault(t => GetId(t) == id.ToString());
        }

        public void Remove(int id)
        {
            Connection.Delete<Produto>(id);
        }

        public void Update(TEntity obj)
        {
            Connection.Update(obj);
        }
        
        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }

        private string GetId(TEntity obj)
        {
            Type t = typeof(TEntity);
            foreach (var property in t.GetRuntimeProperties())
            {
                var attr = (PrimaryKeyAttribute[])property.GetCustomAttributes(typeof(PrimaryKeyAttribute), false);
                if (attr.Length > 0)
                    return property.GetValue(obj).ToString();
            }

            return "";
        }
    }
}
