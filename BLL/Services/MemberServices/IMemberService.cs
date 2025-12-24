using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels.MemberViewModels;

namespace BLL.Services.Member
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMember);

        DetailsMemberViewModel? GetMemberDetails(int MemberId);
        HealthRecordViewModel? GetHealthRecord(int MemberId);

        UpdateMemberViewModel? UpdateToMemberDetails(int MemberId);
        bool UpdateMemberDetails(int Id,UpdateMemberViewModel updateMemberViewModel);
        bool DeleteMember(int MemberId);
    }
}
