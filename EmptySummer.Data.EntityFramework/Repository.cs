using EmptySummer.Core.Interfaces;
using System;
using EmptySummer.Core.Models;
using System.Linq;

namespace EmptySummer.Data.EntityFramework
{
    public class Repository : IRepository, IDisposable
    {
        internal BooksDbContext Context { get; }

        public Repository(string nameOrConnectionString)
            : this(new BooksDbContext(nameOrConnectionString))
        { }
        public Repository(BooksDbContext context) => Context = context;

        public void Add<T>(T entity) where T : Entity => Context.Set<T>().Add(entity);
        public void Delete<T>(T entity) where T : Entity => Context.Set<T>().Remove(entity);

        public T GetById<T>(int id) where T : Entity => Context.Set<T>().Find(id);

        public IQueryable<T> Query<T>() where T : Entity => Context.Set<T>();

        public void Save() => Context.SaveChanges();

        public void Update<T>(T entity) where T : Entity
        {
            T loaded = GetById<T>(entity.Id);
            if (loaded != null)
                Context.Entry(loaded).CurrentValues.SetValues(entity);
        }

        public void Dispose() => Context.Dispose();
    }
}
