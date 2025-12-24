using AutoMapper;
using BLL.Services.AttachmentService;
using BLL.ViewModels.MemberViewModels;
using DAL.Entities.HealthRecord;
using DAL.Entities.MemberEntity;
using DAL.Entities.Plan;
using DAL.Entities.RelationalEntities;
using DAL.Entities.Session;
using DAL.UOfW;

namespace BLL.Services.Member
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachment;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper , IAttachmentService attachment)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.attachment = attachment;
        }

        #region Helper Methods

        private bool EmailExist(string email)
        {
            return unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetAll(x => x.Email == email).Any();
        }

        private bool PhoneExist(string phone)
        {
            return unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetAll(x => x.Phone == phone).Any();
        }

        #endregion

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetAll();
            if (members is null || !members.Any()) return [];

            return mapper.Map<IEnumerable<MemberViewModel>>(members);
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                if (EmailExist(createMember.Email) && PhoneExist(createMember.Phone))
                    return false;
                var PhotoName = attachment.Upload("Member", createMember.PhotoFile);
                if (string.IsNullOrEmpty(PhotoName)) return false;
                var member = new DAL.Entities.MemberEntity.Member()
                {
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Address = new DAL.Entities.Address()
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        Street = createMember.Street,
                        City = createMember.City,

                    },
                    CreatedAt = DateTime.Now,
                    DateOfBirth = createMember.DateOfBirth,
                    Gander = createMember.Gender,
                    UpdatedAt = DateTime.Now,
                    Name = createMember.Name,
                    HealthRecord = new HealthRecord()
                    {
                        Wight = createMember.HealthRecordVM.Wight,
                        Hight = createMember.HealthRecordVM.Hight,
                        Note = createMember.HealthRecordVM.Note,
                        BloodType = createMember.HealthRecordVM.BloodType
                    }
                    
                };
                member.PhotoUrl = PhotoName;
                unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().Add(member);
                var isCreated = unitOfWork.SaveChanges() > 0;
                if (!isCreated) 
                { 
                    attachment.Delete("Member", PhotoName);
                    return false;
                }
                else
                {
                    return isCreated;
                }
            }
            catch
            {
                return false;
            }
        }

        public DetailsMemberViewModel? GetMemberDetails(int memberId)
        {
            var member = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetById(memberId);
            if (member is null) return null;

            var vm = mapper.Map<DetailsMemberViewModel>(member);

            // Get Active Membership
            var active = unitOfWork.GetRepository<MemberShip>()
                .GetAll(x => x.MemberId == memberId && x.Status == "Active")
                .FirstOrDefault();

            if (active != null)
            {
                vm.MemberShipStartDate = active.CreatedAt.ToShortDateString();
                vm.MemberShipEndDate = active.EndDate.ToShortDateString();

                var plan = unitOfWork.GetRepository<Plan>().GetById(active.PlanId);
                vm.PlanName = plan?.Name;
            }

            return vm;
        }

        public HealthRecordViewModel? GetHealthRecord(int memberId)
        {
            var health = unitOfWork.GetRepository<HealthRecord>().GetById(memberId);
            if (health is null) return null;

            return mapper.Map<HealthRecordViewModel>(health);
        }

        public UpdateMemberViewModel? UpdateToMemberDetails(int memberId)
        {
            var member = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetById(memberId);
            if (member is null) return null;

            return mapper.Map<UpdateMemberViewModel>(member);
        }

        public bool UpdateMemberDetails(int id, UpdateMemberViewModel updateMember)
        {
            try
            {
                var EmailExist = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetAll(E => E.Email == updateMember.Email && E.Id != id);
                var PhoneExist = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetAll(E => E.Phone == updateMember.Email && E.Id != id);

                if(EmailExist.Any() && PhoneExist.Any()) return false;

                var member = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetById(id);
                if (member is null) return false;

                mapper.Map(updateMember, member);
                member.UpdatedAt = DateTime.Now;

                unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().Update(member);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMember(int memberId)
        {
            var member = unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().GetById(memberId);
            if (member is null) return false;

            var SessionIds =
                unitOfWork.GetRepository<MemberSession>()
                          .GetAll(x => x.MemberId == memberId)
                          .Select(x=>x.SessionId);

            var HasFutureSession = unitOfWork.GetRepository<Session>().GetAll(
                x => SessionIds.Contains(x.Id) && x.StartDate > DateTime.Now
                ).Any();
                          

            if (HasFutureSession) return false;

            var memberships = unitOfWork.GetRepository<MemberShip>().GetAll(x => x.MemberId == memberId);

            try
            {
                foreach (var membership in memberships)
                {
                    unitOfWork.GetRepository<MemberShip>().Delete(membership);
                }

                unitOfWork.GetRepository<DAL.Entities.MemberEntity.Member>().Delete(member);
                var IsDeleted = unitOfWork.SaveChanges() > 0;
                if (IsDeleted)
                    attachment.Delete("Member",member.PhotoUrl);
                return IsDeleted;

            }
            catch
            {
                return false;
            }
        }
    }
}
