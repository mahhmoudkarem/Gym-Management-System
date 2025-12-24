using System;
using AutoMapper;
using BLL.ViewModels.SessionViewModels;
using DAL.Entities.Category;
using DAL.Entities.Session;
using DAL.Entities.Trainer;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace BLL.MappingProfiles.SessionMappingProfiles
{
    public class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {
            // ------------------------------
            // Session → SessionViewModel (Details / Index)
            // ------------------------------
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore()) // حساب يدوي
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Capcity, opt => opt.MapFrom(src => src.Capacity));

            // ------------------------------
            // Session → UpdateSessionViewModel (Edit form)
            // ------------------------------
            CreateMap<Session, UpdateSessionViewModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.TrainerId));

            // ------------------------------
            // UpdateSessionViewModel → Session (Edit POST)
            // ------------------------------
            CreateMap<UpdateSessionViewModel, Session>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.TrainerId));

            // ------------------------------
            // CreateSessionViewModel → Session (Create POST)
            // ------------------------------
            CreateMap<CreateSessionViewModel, Session>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
                .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.TrainerId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

            // ------------------------------
            // Dropdowns for Trainers and Categories
            // ------------------------------
            CreateMap<Trainer, TrainerSelectViewModel>().ReverseMap();
            CreateMap<Category, CategorySelectViewModel>().ReverseMap();
        }
    }
}
