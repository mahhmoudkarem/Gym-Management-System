using BLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace BLL.Services.SessionServices
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel session);

        UpdateSessionViewModel? GetSessionToUpdate(int id);
        bool UpdateSession(UpdateSessionViewModel session , int id);
        bool DeleteSession(int id);
        IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown();
        IEnumerable<CategorySelectViewModel> GetCategoryForDropDown();

    }
}
