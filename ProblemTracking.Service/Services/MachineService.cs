using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProblemTracking.Entity;
using ProblemTracking.Entity.Entities;
using ProblemTracking.Service.Model;
using System.Collections.Generic;
using System.Linq;
using Mapster;


namespace ProblemTracking.Service.Services
{
    public class MachineService : BaseService<MachineViewModel, int>
    {
        private IConfiguration _configuration { get; }
        public MachineService(ApplicationDbContext coreDbContext, IConfiguration configuration) : base(coreDbContext)
        {
            _configuration = configuration;
        }
        public List<MachineViewModel> GetMachines()
        {
            var machines = coreDbContext.Machine.Include(x => x.InvestigateSteps).ToList();

            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("Machine");
            return machines.Adapt<List<MachineViewModel>>(mapsterConfig);
        }
    }
}
