using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Core.IRespository
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Get(Expression<Func<T , bool>> filter , bool IsTracking = true);
        public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
