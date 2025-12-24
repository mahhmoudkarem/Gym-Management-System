using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Session;
using DAL.Repositories.GenaricRepo;

namespace DAL.Repositories.SessionRepo
{
    public interface ISessionRepository:IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();
        Session? GetSessionWithTrainerAndCategoryById(int id);

        int GetCountOfSessionBoked(int id);
    }
}
