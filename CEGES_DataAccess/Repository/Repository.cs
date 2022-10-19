using CEGES_DataAccess.Persistence;
using CEGES_DataAccess.Repository.IRepository;
using CEGES_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        // pour accéder à la BD, aux entités
        private readonly ApplicationDbContext _db;
        // pour faire des changements directs
        public  DbSet<T> dbset;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
         
            this.dbset = _db.Set<T>();

        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public async Task<T> GetAsync(int id)
        {
            return await dbset.FindAsync(id);
        }


        /* doit tenir compte que certains retournent des VM avec des where et des liens
           Exemple: IEnumerable<Product> objList = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType);
        Expression<Func> permet le linq avec filter permet le where
        IOrderedQueryable... permet le orderby
        includeProperties permet le lié avec Include
        isTracking, par défaut dans EF Core, utile mais diminue la performance. Pour Retreive seulement: peut être false
      */
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // reproduit: _db.Product.Include(u => u.Category).Include(u => u.ApplicationType)
                    // mais passé en string
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // reproduit: _db.Product.Include(u => u.Category).Include(u => u.ApplicationType)
                    // mais passé en string
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        // Pas de Async pour Remove le Async dans le nom est pour garder le standard et différencier le Repo standard et le Repo Async
        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            // Supprimer plusieurs lignes en même temps
            dbset.RemoveRange(entity);
        }

    }
}
