using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System.Linq.Expressions;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class //T'nin class ifadesini where ile yazmak zorundayız. FCore class'lar ile çalıştığı için belirtmek zorundayız.
    {
        protected readonly AppDbContext _context; //private yerine protected olmasının sebebi ek metodlara ihtiyaç olursa AppDbContext'e erişmek gerekiyor. Miras  aldığımız yerde erişmek için.
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context) //readonly olarak tanımlandığı set olarak ctor'da değerini atadık.
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity) //geriye bir değer dönmeyeceği için return yerine await yazdık.
        {
            await _dbSet.AddAsync(entity);


        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable(); //AsNoTracking dememizin sebebi EfCore çekmiş olduğu dataları memory'e almasın .
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            //_context.Entry(entity).State=EntityState.Deleted aşağıdaki ile aynıdır.
            _dbSet.Remove(entity); //Async olmamasının sebebi , SaveChanges diyene kadar state'de delete olarak işaretliyor. SaveChanges dersek , DB'den siliyor.
                                   //O yüzden Async'ye ihtiyaç yok.
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
