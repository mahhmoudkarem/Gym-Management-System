using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.GenaricRepo;
using DAL.Repositories.SessionRepo;

namespace DAL.UOfW
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : Entities.BaseEntity , new();

        int SaveChanges();
    }
}
