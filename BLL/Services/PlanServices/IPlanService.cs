using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels.PlanViewModels;

namespace BLL.Services.PlanServices
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetPlans();
        PlanViewModel GetPlanById(int id);
        UpdatePlanViewModel? GetPlanToUpdate(int id);

        bool UpDatePlan(int id,UpdatePlanViewModel plan);
        bool ToggledPlan(int id);
    }
}
