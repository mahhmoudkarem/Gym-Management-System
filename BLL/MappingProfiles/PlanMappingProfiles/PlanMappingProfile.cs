using AutoMapper;
using BLL.ViewModels.PlanViewModels;
using DAL.Entities.Plan;

namespace BLL.MappingProfiles
{
    public class PlanMappingProfile : Profile
    {
        public PlanMappingProfile()
        {
            /******************************
             * Plan → PlanViewModel
             ******************************/
            CreateMap<Plan, PlanViewModel>();

            /******************************
             * Plan → UpdatePlanViewModel
             ******************************/
            CreateMap<Plan, UpdatePlanViewModel>()
                .ForMember(dest => dest.DurationDay,
                    opt => opt.MapFrom(src => src.DurationDays));

            /******************************
             * UpdatePlanViewModel → Plan
             ******************************/
            CreateMap<UpdatePlanViewModel, Plan>()
                .ForMember(dest => dest.DurationDays,
                    opt => opt.MapFrom(src => src.DurationDay))
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));

            /******************************
             * PlanViewModel → Plan
             ******************************/
            CreateMap<PlanViewModel, Plan>()
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));
        }
    }
}
