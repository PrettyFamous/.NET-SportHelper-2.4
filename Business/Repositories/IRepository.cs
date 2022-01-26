using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IRepository<T, TKey> : IRepositoryBase where T : class
    {
        T Read(TKey key);
        Task<T> ReadAsync(TKey key);

        void Create(T value);
        Task CreateAsync(T value);
        void Create(IEnumerable<T> values);
        Task CreateAsync(IEnumerable<T> values);

        void Update(T value);
        void UpdateFields(T value, params Expression<Func<T, object>>[] includeProperties);
        void Update(IEnumerable<T> values);

        void CreateOrUpdate(T value);
        Task CreateOrUpdateAsync(T value);
        void CreateOrUpdate(IEnumerable<T> values);
        Task CreateOrUpdateAsync(IEnumerable<T> values);

        void Delete(T value);
        void Delete(IEnumerable<T> values);

        List<T> Query(Expression<Func<T, bool>> where = null);
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> where = null);
        TResult Query<TResult>(Func<IQueryable<T>, TResult> body);
    }
}
