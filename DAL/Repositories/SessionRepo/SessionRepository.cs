using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Contexts;
using DAL.Entities.Session;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.SessionRepo
{
    public class SessionRepository : GenaricRepo.GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext context;

        public SessionRepository(GymDbContext context):base(context)
        {
            this.context = context;
        }
        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return  context.Sessions.Include(s => s.Category)
                                     .Include(s => s.Trainer)
                                     .ToList() ?? [];
        }

        public int GetCountOfSessionBoked(int id)
        {
            return context.MemberSessions.Count(s => s.SessionId == id);

        }

        public Session? GetSessionWithTrainerAndCategoryById(int id)
        {
            return context.Sessions.Include(s => s.Category)
                                     .Include(s => s.Trainer)
                                     .FirstOrDefault(s => s.Id == id);
        }
    }
}
