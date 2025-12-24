using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels.TrainerViewModels;

namespace BLL.Services.TrainerServices
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel model);
        TrainerDetailsViewModel? GetTrainerDetails(int TrainerId);

        UpdateTrainerViewModel? GetTrainerToUpdated(int TrainerId);
        bool UpdateTrainer(int TrainerId, UpdateTrainerViewModel updateTrainer);
        bool DeleteTrainer(int TrainerId);

    }
}
