using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ViewModels.SessionViewModels;
using DAL.Entities.Category;
using DAL.Entities.Session;
using DAL.Entities.Trainer;
using DAL.UOfW;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace BLL.Services.SessionServices
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        #region HelperMethod

        private bool TrainerExist(int id)
        {
            return unitOfWork.GetRepository<Trainer>().GetById(id) != null;
        }
        private bool CategoryExist(int id)
        {
            return unitOfWork.GetRepository<Category>().GetById(id) != null;
        }

        private bool DateTimeValid(DateTime StartDate, DateTime EndDate)
        {
            return EndDate > StartDate;
        }

        private bool IsSessionAvailableToUpdate(Session session)
        {
            if (session == null) return false;
            if (DateTime.Now > session.EndDate) return false;
            if(session.StartDate <= DateTime.Now) return false;
            var HasActiveSessions = unitOfWork.SessionRepository.GetCountOfSessionBoked(session.Id) > 0;
            if (HasActiveSessions) return false;
            return true;
            
       

        }
        private bool IsSessionAvailableToDeleting(Session session)
        {
            if (session == null) return false;
            if(session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now )  return false;
            if(session.StartDate > DateTime.Now) return false;

            var HasActiveSessions = unitOfWork.SessionRepository.GetCountOfSessionBoked(session.Id) > 0;
            if (HasActiveSessions) return false;
            return true;
            
       

        }


        #endregion
        public bool CreateSession(CreateSessionViewModel session)
        {

            try
            {
                //Trainer is already exist
                if (!CategoryExist(session.CategoryId)) return false;
                //Category is already exist
                if (!TrainerExist(session.TrainerId)) return false;
                //StartDate Before EndDate
                if (!DateTimeValid(session.StartDate, session.EndDate)) return false;
                // Check Capcity
                if (session.Capacity > 25 || session.Capacity < 0) return false;
                var catName = unitOfWork.SessionRepository.GetById(session.CategoryId);

                var mapped = new Session()
                {
                    TrainerId = session.TrainerId,
                    Capacity = session.Capacity,
                    CategoryId = session.CategoryId,
                    Descripcion = session.Description,
                    StartDate = session.StartDate,
                    EndDate = session.EndDate,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,

                };
                unitOfWork.SessionRepository.Add(mapped);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex) {
                Console.WriteLine($"{ex}");
                return false;
            }

        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = unitOfWork.SessionRepository.GetAllSessionWithTrainerAndCategory();
            if (!sessions.Any()) return [];  
            
            var mapped = mapper.Map<IEnumerable<Session> ,IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in mapped)
            {
                session.AvailableSlots = session.Capcity - unitOfWork.SessionRepository.GetCountOfSessionBoked(session.Id);
            }
            return mapped;
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var session = unitOfWork.SessionRepository.GetSessionWithTrainerAndCategoryById(id);
            if (session == null) return null;

            var mapped = mapper.Map<Session, SessionViewModel>(session);

            mapped.AvailableSlots = mapped.Capcity - unitOfWork.SessionRepository.GetCountOfSessionBoked(mapped.Id);
            
            return mapped;
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int id)
        {
            try
            {
                var session = unitOfWork.SessionRepository.GetSessionWithTrainerAndCategoryById(id);
                if (!IsSessionAvailableToUpdate(session!)) return null;
                return mapper.Map<UpdateSessionViewModel>(session);
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                return null;
            }

        }

        public bool UpdateSession(UpdateSessionViewModel session, int id)
        {
            try
            {
                var s = unitOfWork.SessionRepository.GetById(id);
                if (!IsSessionAvailableToUpdate(s!)) return false;

                // صححنا هنا
                if (!CategoryExist(s!.CategoryId)) return false;
                if (!TrainerExist(s!.TrainerId)) return false;

                if (!DateTimeValid(session.StartDate, session.EndDate)) return false;

                mapper.Map(session, s);
                s.UpdatedAt = DateTime.Now;
                unitOfWork.SessionRepository.Update(s);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        public bool DeleteSession(int id)
        {
            try
            {
                var session = unitOfWork.SessionRepository.GetById(id);
                if (!IsSessionAvailableToDeleting(session!)) return false;
                unitOfWork.SessionRepository.Delete(session!);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown()
        {
            var trainers = unitOfWork.GetRepository<Trainer>().GetAll() ?? new List<Trainer>();
            return mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoryForDropDown()
        {
            var cats = unitOfWork.GetRepository<Category>().GetAll() ?? new List<Category>();
            return mapper.Map<IEnumerable<CategorySelectViewModel>>(cats);
        }

    }
}
