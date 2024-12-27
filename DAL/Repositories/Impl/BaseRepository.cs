using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


    namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public class BaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList(); 
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }

}
