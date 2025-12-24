using AutoMapper;
using BLL.Services.TrainerServices;
using BLL.ViewModels.TrainerViewModels;
using DAL.Entities.Session;
using DAL.Entities.Trainer;
using DAL.UOfW;

public class TrainerService : ITrainerService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    private bool EmailExist(string email)
    {
        return unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
    }

    private bool PhoneExist(string phone)
    {
        return unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone).Any();
    }

    public IEnumerable<TrainerViewModel> GetAllTrainers()
    {
        var trainers = unitOfWork.GetRepository<Trainer>().GetAll();
        if (trainers is null) return [];

        return mapper.Map<IEnumerable<TrainerViewModel>>(trainers);
    }

    public TrainerDetailsViewModel? GetTrainerDetails(int trainerId)
    {
        var trainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
        if (trainer is null) return null;

        return mapper.Map<TrainerDetailsViewModel>(trainer);
    }

    public bool CreateTrainer(CreateTrainerViewModel model)
    {
        if (EmailExist(model.Email) || PhoneExist(model.Phone)) return false;

        try
        {
            var trainer = mapper.Map<Trainer>(model);
            unitOfWork.GetRepository<Trainer>().Add(trainer);
            return unitOfWork.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public UpdateTrainerViewModel? GetTrainerToUpdated(int trainerId)
    {
        var trainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
        if (trainer is null) return null;

        return mapper.Map<UpdateTrainerViewModel>(trainer);
    }

    public bool UpdateTrainer(int trainerId, UpdateTrainerViewModel model)
    {
        try
        {
            var EmailExist = unitOfWork.GetRepository<Trainer>().GetAll(E => E.Email == model.Email && E.Id != trainerId);
            var PhoneExist = unitOfWork.GetRepository<Trainer>().GetAll(E => E.Phone == model.Email && E.Id != trainerId);

            if (EmailExist.Any() && PhoneExist.Any()) return false;

            var trainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return false;

            mapper.Map(model, trainer);

            // Address updated manually because it's nested object
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Address.Street = model.Street;
            trainer.Address.City = model.City;

            trainer.UpdatedAt = DateTime.Now;

            unitOfWork.GetRepository<Trainer>().Update(trainer);
            return unitOfWork.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteTrainer(int trainerId)
    {
        var trainer = unitOfWork.GetRepository<Trainer>().GetById(trainerId);
        var hasSessions = unitOfWork.GetRepository<Session>()
            .GetAll(x => x.TrainerId == trainerId && x.StartDate > DateTime.Now).Any();

        if (trainer is null || hasSessions) return false;

        unitOfWork.GetRepository<Trainer>().Delete(trainer);
        return unitOfWork.SaveChanges() > 0;
    }
}
