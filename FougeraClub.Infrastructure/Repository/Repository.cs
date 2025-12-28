using FougeraClub.Core.IRespository;
using FougeraClub.Core.Shared;
using FougeraClub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _db;
        public Repository(ApplicationContext context)
        {
            _context = context;
            _db = _context.Set<T>(); 
        }
        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public void Delete(T entity)
        {
            if(entity is ISoftDeletable softentity)
            {
                softentity.IsDeleted = true;
                softentity.DeletedAt = DateTime.Now;
                _db.Update(entity);
            }
            _db.Remove(entity);
            
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, bool IsTracking = true)
        {
            IQueryable<T> query = _db;
            if (!IsTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _db;
            if (filter != null)
                query = query.Where(filter);
            
            return await query.ToListAsync();
        }

        public void Update(T entity)
        {
            _db.Update(entity);
        }
    }
}
