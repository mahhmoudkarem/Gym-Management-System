using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Entities.MemberEntity;

namespace DAL.Repositories.GenaricRepo
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        //Get All
        IEnumerable<TEntity> GetAll(Func<TEntity,bool>? func = null);
        //GetById
        TEntity? GetById(int Id);

        //Add
        void Add(TEntity entity);
        //Update
        void Update(TEntity entity);
        //Delete
        void Delete(TEntity entity);
    }
}
