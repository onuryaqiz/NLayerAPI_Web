using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);


        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        //T  x'e denk geliyor.
        // veri tabanına gitmez             // ToList ile DB'ye gider.
        // productRepository.where(x=>x.id>5).ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //Yazılmış olan sorgular direkt veri tabanına gitmez. ToList() methodu veri tabanına gider.
                                                                   //Daha performanslı çalışabilmek için IQuerable ile yazdık.

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); //true veya false döner.
        Task AddAsync(T entity); //Burada uzun bir süreç olduğu için Async oldu. 

        Task AddRangeAsync(IEnumerable<T> entities); //İstediğimiz bir tipe dönüştürebiliriz. 

        void Update(T entity); //frcore tarafında async yoktur. State'i değiştirdiği için async olmasına gerek yok. Uzun bir işlem olmadığı için.

        void Remove(T entity); //frcore tarafında async yoktur. State'i değiştirdiği için async olmasına gerek yok. 

        void RemoveRange(IEnumerable<T> entities);
    }
}
