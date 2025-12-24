using AutoMapper;
using BLL.ViewModels.TrainerViewModels;
using DAL.Entities.Trainer;
using DAL.Entities;

public class TrainerMappingProfile : Profile
{
    public TrainerMappingProfile()
    {
        // Trainer → TrainerViewModel
        CreateMap<Trainer, TrainerViewModel>()
            .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialties.ToString()));

        // Trainer → TrainerDetailsViewModel
        CreateMap<Trainer, TrainerDetailsViewModel>()
            .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialties.ToString()))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

        // Trainer → UpdateTrainerViewModel
        CreateMap<Trainer, UpdateTrainerViewModel>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

        // CreateTrainerViewModel → Trainer
        CreateMap<CreateTrainerViewModel, Trainer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                BuildingNumber = src.BuildingNumber,
                Street = src.Street,
                City = src.City
            }));

        // UpdateTrainerViewModel → Trainer (no Address creation here!)
        CreateMap<UpdateTrainerViewModel, Trainer>()
            .ForMember(dest => dest.Address, opt => opt.Ignore()); // Address updated manually
    }
}
