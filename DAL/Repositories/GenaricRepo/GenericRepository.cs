using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Contexts;
using DAL.Entities;
using DAL.Entities.Session;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.GenaricRepo
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext dbContext;

        public GenericRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);


        public void Delete(TEntity entity)
        {
            // Attach if entity is not tracked
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbContext.Set<TEntity>().Attach(entity);
            }

            dbContext.Set<TEntity>().Remove(entity);
        }


        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? func = null)
        {
            if (func == null)
                return dbContext.Set<TEntity>().AsNoTracking().ToList();
            else
                return dbContext.Set<TEntity>().AsNoTracking().Where(func).ToList();

        }

        public TEntity? GetById(int Id) => dbContext.Set<TEntity>().Find(Id);

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

    }
}
