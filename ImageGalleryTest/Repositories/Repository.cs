using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingTest.Domain.Models;
using AccountingTest.Infrastructure.Abstractions;
using ImageGalleryTest;
using Microsoft.EntityFrameworkCore;

namespace AccountingTest.Infrastructure.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext context;
        protected DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public T Get(string id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public async Task<T> GetAsync(string id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> CreateBatchAsync(IEnumerable<T> entitiesToCreate)
        {
            entities.AddRange(entitiesToCreate);
            await context.SaveChangesAsync();

            return entities;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync();

            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            entities.RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }
}
