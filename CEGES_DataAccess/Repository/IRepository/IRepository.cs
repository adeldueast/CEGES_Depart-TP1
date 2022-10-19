using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_DataAccess.Repository.IRepository
{
    public  interface IRepository<T> where T : class
    {
        // Les méthodes devant être implantées dans les repositories
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null
        );


        // Retourne le 1er seulement
        Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null
        );

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}
