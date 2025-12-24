using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels.AnalyticsServices;
using DAL.Entities.RelationalEntities;
using DAL.Entities.Trainer;
using DAL.UOfW;

namespace BLL.Services.AnalyticsServices
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalytics()
        {
            var sessions = unitOfWork.SessionRepository.GetAllSessionWithTrainerAndCategory();
            return new AnalyticsViewModel()
            {
                ActiveMember = unitOfWork.GetRepository<MemberShip>().GetAll(m => m.Status == "Active").Count(),
                TotalMember = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetAll().Count(),
                TotalTrainers = unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                CompletedSessions = sessions.Count(x => x.EndDate < DateTime.Now),
                UpcomingSessions = sessions.Count(x => x.StartDate > DateTime.Now),
                OngoingSessions = sessions.Count(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now),

            };
        }
    }
}
