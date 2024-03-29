﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProblemTracking.Web.Model;
using ProblemTracking.Web.Services;
using System.Collections.Generic;

namespace ProblemTracking.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MachineController :   BaseController<MachineService>
    {
        private readonly IConfiguration _config;
        public MachineController(IConfiguration config, IServiceFactory serviceFactory) : base(serviceFactory)
        {
            _config = config;
        }
        [HttpGet("getMachines")]
        [Authorize(Policy = "User")]
        public List<MachineViewModel> GetMachines()
        {
            return Service.GetMachines();
        }
        [HttpGet("getInvestigateStep")]
        [Authorize(Policy = "User")]
        public List<InvestigateStepViewModel> GetInvestigateStep(int machineId)
        {
            return Service.GetInvestigateStep(machineId);
        }
    }
}
