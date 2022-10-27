using Microsoft.Extensions.Configuration;
using ProblemTracking.Entity;
using ProblemTracking.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ProblemTracking.Entity.Entities;

namespace ProblemTracking.Service.Services
{
    public class ProblemService : BaseService<ProblemViewModel,int>
    {
        private IConfiguration _configuration { get; }
        public ProblemService(ApplicationDbContext coreDbContext, IConfiguration configuration) : base(coreDbContext)
        {
            _configuration = configuration;
        }

        public List<ProblemViewModel> GetProblems() {

            var results = coreDbContext.Problem.Include(x=>x.User).ToList();
            var resultView = results.Adapt<List<ProblemViewModel>>();
            var investigates = coreDbContext.ProblemInvestigate.AsNoTracking().Include(x => x.StepToSolved).Include(x=>x.Problem).ToList();
            resultView.ForEach(eachResult => { 
            var investigate = investigates.FirstOrDefault(x => x.Problem?.Id == eachResult.Id);
                if (investigate != null) {
                    eachResult.SolveStatus = investigate.SolveStatus;
                    eachResult.SuceesInvestigateName = investigate?.StepToSolved?.StepName;
                }
             
            });

            return resultView;
        }
        public int SaveProblem(ProblemViewModel problemView) {
            var problem = problemView.Adapt<Problem>();
            if (problem.User != null)
            {
                problem.User = coreDbContext.User.First(x => x.UserName == problem.User.UserName);
            }

            coreDbContext.Problem.Add(problem);
            coreDbContext.SaveChanges();
            return problem.Id;
        }
        public string SaveProblemProblemInvestigate(ProblemInvestigateViewModel problemInvestigateView)
        {
            var problemInvestigate = problemInvestigateView.Adapt<ProblemInvestigate>();
            if (problemInvestigate.StepToSolved != null)
            {
                problemInvestigate.StepToSolved = coreDbContext.InvestigateStep.First(x => x.Id == problemInvestigate.StepToSolved.Id);
            }
            if (problemInvestigate.Problem != null)
            {
                problemInvestigate.Problem = coreDbContext.Problem.First(x => x.Id == problemInvestigate.Problem.Id);
            }
            //problemInvestigate.Id = Guid.NewGuid();

            coreDbContext.ProblemInvestigate.Add(problemInvestigate);
            coreDbContext.SaveChanges();
            return problemInvestigate.Id.ToString();
        }
    }


}
