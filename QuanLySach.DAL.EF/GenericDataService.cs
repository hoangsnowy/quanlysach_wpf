using Microsoft.EntityFrameworkCore;
using QuanLySach.DAL.EF.Models;
using QuanLySach.DAL.Interfaces;

namespace QuanLySach.DAL.EF
{
    public class GenericDataService<T> : IDataService<T> where T : EntityBase
    {
        protected readonly BookDbContextFactory _contextFactory;
        protected readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(BookDbContextFactory contextFactory, NonQueryDataService<T> nonQueryDataService)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> Add(T entity)
        {
            return await _nonQueryDataService.Add(entity);
        }

        public void AddRange(IList<T> entities)
        {
            _nonQueryDataService.AddRange(entities);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public int Count()
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                return context.Set<T>().Count();
            }
        }

        public async Task<T> Get(int id)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                Guard.ThrowIfObjectNotFound(entity, id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
