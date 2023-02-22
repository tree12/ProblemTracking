using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProblemTracking.Entity;
using ProblemTracking.Entity.Entities;
using ProblemTracking.Web.Model;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using ProblemTracking.Repository;


namespace ProblemTracking.Web.Services
{
    public class MachineService : BaseService<MachineViewModel, int>
    {
        private IConfiguration _configuration { get; }
        public MachineService(UnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            _configuration = configuration;
        }
        public List<MachineViewModel> GetMachines()
        {
            var machines = unitOfWork.MachineRepository.Get(null, null, nameof(Machine.InvestigateSteps)).ToList();

            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("Machine");
            return machines.Adapt<List<MachineViewModel>>(mapsterConfig);
        }
        public List<InvestigateStepViewModel> GetInvestigateStep(int machineId)
        {
            var investigateSteps = unitOfWork.InvestigateStepRepository.Get(x => x.MachineId == machineId).ToList();
            return investigateSteps.Adapt<List<InvestigateStepViewModel>>();
        }
    }
}
