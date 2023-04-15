using CqrsFramework.Infrastructure.Abstract;
using System.Linq.Expressions;

namespace CqrsFramework.Infrastructure.DataAccess.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList();
        List<T>? Search(Expression<Func<T, bool>> filter);
        T? GetItem(Expression<Func<T, bool>>? filter);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
