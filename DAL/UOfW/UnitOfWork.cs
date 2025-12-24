using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Contexts;
using DAL.Entities;
using DAL.Repositories.GenaricRepo;
using DAL.Repositories.SessionRepo;

namespace DAL.UOfW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext context;
        private readonly ISessionRepository sessionRepository;
        private readonly Dictionary<Type , object> Repos = new();

        public UnitOfWork(GymDbContext context , ISessionRepository sessionRepository)
        {
            this.context = context;
            this.sessionRepository = sessionRepository;
        }

        public ISessionRepository SessionRepository => sessionRepository;

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);
            if (Repos.TryGetValue(EntityType, out var Repo))
                return (IGenericRepository<TEntity>)Repo;
            var NewRepo = new GenericRepository<TEntity>(context);
            Repos[EntityType] = NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
