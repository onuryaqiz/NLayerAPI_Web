using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();


        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity); //IGenericRepository'de Async yoktu.
                                    //Fakat burada Async olduğu için Task koyduk. Sebebi ise ; Veri tabanına gidip işlem yapacağı için async olmak zorunda !

        Task RemoveAsync(T entity); //IGenericRepository'de Async yoktu.
                                    //Fakat burada Async olduğu için Task koyduk. Sebebi ise ; Veri tabanına gidip işlem yapacağı için async olmak zorunda !

        Task RemoveRangeAsync(IEnumerable<T> entities);//IGenericRepository'de Async yoktu.
                                                       //Fakat burada Async olduğu için Task koyduk. Sebebi ise ; Veri tabanına gidip işlem yapacağı için async olmak zorunda !
    }
}
