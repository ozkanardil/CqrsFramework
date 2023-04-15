using Microsoft.EntityFrameworkCore;
using CqrsFramework.Infrastructure.Abstract;
using CqrsFramework.Infrastructure.DataAccess.Abstract;
using System.Linq.Expressions;


namespace CqrsFramework.Infrastructure.DataAccess.Concrete
{
    public class EfRepository<T, TContext> : IRepository<T>
         where T : class, IEntity, new()
         where TContext : DbContext, new()
    {
        public int Add(T entity)
        {
            int addedCount = 0;
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                addedCount = context.SaveChanges();
            }
            return addedCount;
        }

        public int Delete(T entity)
        {
            int deletedCount = 0;
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                deletedCount = context.SaveChanges();
            }
            return deletedCount;
        }

        public T? GetItem(Expression<Func<T, bool>>? filter)
        {
            using (var context = new TContext())
            {
                return context.Set<T>().SingleOrDefault(filter);
            }
        }

        public List<T> GetList()
        {
            using (var context = new TContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public List<T>? Search(Expression<Func<T, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<T>().Where(filter).ToList();
            }
        }

        public int Update(T entity)
        {
            int updatedCount = 0;
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                updatedCount = context.SaveChanges();
            }
            return updatedCount;
        }
    }
}
