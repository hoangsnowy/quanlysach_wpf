using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF
{
    public class NonQueryDataService<T> where T : EntityBase
    {
        private readonly BookDbContextFactory _contextFactory;

        public NonQueryDataService(BookDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Add(T entity)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public void AddRange(IList<T> entities)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                context.Set<T>().AddRange(entities);
                context.SaveChanges();
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (BookDbContext context = _contextFactory.CreateDbContext())
            {
                T? entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                Guard.ThrowIfObjectNotFound(entity, id);

                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
