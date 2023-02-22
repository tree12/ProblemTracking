using System;
using ProblemTracking.Entity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProblemTracking.Web.Services;
using ProblemTracking.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ProblemTracking.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProblemController :  BaseController<ProblemService>
    {
        private readonly IConfiguration _config;
        public ProblemController(IConfiguration config, IServiceFactory serviceFactory) : base(serviceFactory)
        {
            _config = config;
        }
        [HttpGet("getAllProblems")]
        [Authorize(Policy = "Admin")]
        public List<ProblemViewModel> GetAllProblems()
        {
            var problems = Service.GetProblems();
            return problems;
        }
        [HttpGet("getProblemsByUser")]
        [Authorize(Policy = "User")]
        public List<ProblemViewModel> GetProblemsByUser(string userName)
        {
            var problems = Service.GetProblems(userName);
            return problems;
        }
        [HttpPost("addProblem")]
        [Authorize(Policy = "User")]
        public int AddProblem([FromBody] ProblemViewModel problem)
        {
            problem.User = new UserViewModel() { UserName = User.Identity.Name };
            int lastId = Service.SaveProblem(problem);
            return lastId;
        }
        [HttpPost("addProblemInvestigate")]
        [Authorize(Policy = "User")]
        public string AddProblemInvestigate([FromBody] ProblemInvestigateViewModel problemInvestigate)
        {
            string lastId = Service.SaveProblemProblemInvestigate(problemInvestigate);
            return lastId;
        }
    }
}
