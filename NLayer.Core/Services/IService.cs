using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();


        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity); //IGenericRepository'de Async yoktu.
                                    //Fakat burada Async olduğu için Task koyduk. Sebebi ise ; Veri tabanına gidip işlem yapacağı için async olmak zorunda !

        Task RemoveAsync(T entity); //IGenericRepository'de Async yoktu.
                                    //Fakat burada Async olduğu için Task koyduk. Sebebi ise ; Veri tabanına gidip işlem yapacağı için async olmak zorunda !

        Task RemoveRangeAsync(IEnumerable<T> entities);//IGenericRepository'de Async yoktu.
                                                       //Fakat burada Async olduğu için Task koyduk. Sebebi ise ; Veri tabanına gidip işlem yapacağı için async olmak zorunda !
    }
}
