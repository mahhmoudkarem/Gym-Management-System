using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ViewModels.MemberViewModels;
using DAL.Entities;
using DAL.Entities.HealthRecord;
using DAL.Entities.MemberEntity;

namespace BLL.MappingProfiles.MemberMappingProfiles
{

        public class MemberMappingProfile : Profile
        {
            public MemberMappingProfile()
            {
                /******************************
                 * Member → MemberViewModel
                 ******************************/
                CreateMap<Member, MemberViewModel>()
                    .ForMember(dest => dest.Gender,
                        opt => opt.MapFrom(src => src.Gander.ToString()));

                /******************************
                 * CreateMemberViewModel → Member
                 ******************************/
                CreateMap<CreateMemberViewModel, Member>()
                    .ForMember(dest => dest.Gander,
                        opt => opt.MapFrom(src => src.Gender))
                    .ForMember(dest => dest.DateOfBirth,
                        opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
                    .ForMember(dest => dest.Address,
                        opt => opt.MapFrom(src => new Address
                        {
                            BuildingNumber = src.BuildingNumber,
                            Street = src.Street,
                            City = src.City
                        }))
                    .ForMember(dest => dest.HealthRecord,
                        opt => opt.MapFrom(src => src.HealthRecordVM));

                /******************************
                 * HealthRecordVM ↔ HealthRecord
                 ******************************/
                CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

                /******************************
                 * Member → UpdateMemberViewModel
                 ******************************/
                CreateMap<Member, UpdateMemberViewModel>()
                    .ForMember(dest => dest.BuildingNumber,
                        opt => opt.MapFrom(src => src.Address.BuildingNumber))
                    .ForMember(dest => dest.Street,
                        opt => opt.MapFrom(src => src.Address.Street))
                    .ForMember(dest => dest.City,
                        opt => opt.MapFrom(src => src.Address.City));

                /******************************
                 * UpdateMemberViewModel → Member
                 ******************************/
                CreateMap<UpdateMemberViewModel, Member>()
                    .ForMember(dest => dest.Address,
                        opt => opt.MapFrom(src => new Address
                        {
                            BuildingNumber = src.BuildingNumber,
                            Street = src.Street,
                            City = src.City
                        }))
                    .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));

                /******************************
                 * Member → DetailsMemberViewModel
                 ******************************/
                CreateMap<Member, DetailsMemberViewModel>()
                    .ForMember(dest => dest.Gender,
                        opt => opt.MapFrom(src => src.Gander.ToString()))
                    .ForMember(dest => dest.DateOfBirth,
                        opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                    .ForMember(dest => dest.Address,
                        opt => opt.MapFrom(src =>
                            $"{src.Address.BuildingNumber},{src.Address.Street},{src.Address.City}"))
                    .ForMember(dest => dest.PlanName, opt => opt.Ignore())
                    .ForMember(dest => dest.MemberShipStartDate, opt => opt.Ignore())
                    .ForMember(dest => dest.MemberShipEndDate, opt => opt.Ignore());
            }
        }
    }

