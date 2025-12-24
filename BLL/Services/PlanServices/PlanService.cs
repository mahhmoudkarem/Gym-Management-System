using AutoMapper;
using BLL.ViewModels.PlanViewModels;
using DAL.Entities.Plan;
using DAL.Entities.RelationalEntities;
using DAL.UOfW;

namespace BLL.Services.PlanServices
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public PlanViewModel? GetPlanById(int id)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null) return null;

            return mapper.Map<PlanViewModel>(plan);
        }

        public IEnumerable<PlanViewModel> GetPlans()
        {
            var plans = unitOfWork.GetRepository<Plan>().GetAll();
            if (plans is null || !plans.Any()) return [];

            return mapper.Map<IEnumerable<PlanViewModel>>(plans);
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int id)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(id);

            if (plan is null || !plan.IsActive || HasActiveMemberShip(id))
                return null;

            return mapper.Map<UpdatePlanViewModel>(plan);
        }

        public bool UpDatePlan(int id, UpdatePlanViewModel planVM)
        {
            try
            {
                var plan = unitOfWork.GetRepository<Plan>().GetById(id);
                if (plan is null || HasActiveMemberShip(id)) return false;

                // Apply AutoMapper updates
                mapper.Map(planVM, plan);
                plan.UpdatedAt = DateTime.Now;

                unitOfWork.GetRepository<Plan>().Update(plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool ToggledPlan(int id)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(id);

            if (plan is null || HasActiveMemberShip(id))
                return false;

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;

            try
            {
                unitOfWork.GetRepository<Plan>().Update(plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Helper

        private bool HasActiveMemberShip(int id)
        {
            return unitOfWork.GetRepository<MemberShip>()
                .GetAll(x => x.PlanId == id && x.Status == "Active")
                .Any();
        }

        #endregion
    }
}
