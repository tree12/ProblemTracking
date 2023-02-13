using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProblemTracking.Entity;
using ProblemTracking.Repository.Interfaces;

namespace ProblemTracking.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext applicationDbContext { get; set; }
        public RepositoryBase(ApplicationDbContext repositoryContext)
        {
            applicationDbContext = repositoryContext;
        }
        public IQueryable<T> FindAll() => applicationDbContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            applicationDbContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => applicationDbContext.Set<T>().Add(entity);
        public void Update(T entity) => applicationDbContext.Set<T>().Update(entity);
        public void Delete(T entity) => applicationDbContext.Set<T>().Remove(entity);
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", bool isTracking = true)
        {
            IQueryable<T> query = applicationDbContext.Set<T>();

            if (filter != null)
            {
                query = isTracking? query.Where(filter): query.Where(filter).AsNoTracking();
            }

            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
