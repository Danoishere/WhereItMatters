using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WhereItMatters.DataAccess
{
    public interface IRepository<T>
    {
        Task Insert(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
    }
}
