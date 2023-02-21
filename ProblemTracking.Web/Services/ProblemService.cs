using Microsoft.Extensions.Configuration;
using ProblemTracking.Entity;
using ProblemTracking.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ProblemTracking.Entity.Entities;
using ProblemTracking.Repository;

namespace ProblemTracking.Web.Services
{
    public class ProblemService : BaseService<ProblemViewModel,int>
    {
        private IConfiguration _configuration { get; }
        public ProblemService(UnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            _configuration = configuration;
        }

        public List<ProblemViewModel> GetProblems(string userName = "")
        {

            var results = string.IsNullOrEmpty(userName) ? unitOfWork.ProblemRepository.Get(null, null, nameof(Problem.User)).ToList(): unitOfWork.ProblemRepository.Get(x=>x.User.UserName == userName, null, nameof(Problem.User)).ToList();
            var resultView = results.Adapt<List<ProblemViewModel>>();
            var investigates = unitOfWork.ProblemInvestigateRepository.Get(null,null,nameof(ProblemInvestigate.StepToSolved)+","+ nameof(ProblemInvestigate.Problem), false).ToList();
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
                problem.User = unitOfWork.UserRepository.FindByCondition(x => x.UserName == problem.User.UserName).FirstOrDefault();
            }

            if (problem.Machine != null)
            {
                problem.Machine = unitOfWork.MachineRepository.FindByCondition(x => x.Id == problemView.Machine.Id)
                    .FirstOrDefault();
            }

            unitOfWork.ProblemRepository.Create(problem);
            unitOfWork.context.Entry(problem.User).State = EntityState.Unchanged;
            unitOfWork.context.Entry(problem.Machine).State = EntityState.Unchanged;
            unitOfWork.Save();
            return problem.Id;
        }
        public string SaveProblemProblemInvestigate(ProblemInvestigateViewModel problemInvestigateView)
        {
            var problemInvestigate = problemInvestigateView.Adapt<ProblemInvestigate>();
            if (problemInvestigate.StepToSolved != null)
            {
                problemInvestigate.StepToSolved = unitOfWork.InvestigateStepRepository.FindByCondition(x => x.Id == problemInvestigate.StepToSolved.Id).FirstOrDefault();
            }
            if (problemInvestigate.Problem != null)
            {
                problemInvestigate.Problem = unitOfWork.ProblemRepository
                    .FindByCondition(x => x.Id == problemInvestigate.Problem.Id)
                    .FirstOrDefault(); 
            }

            unitOfWork.ProblemInvestigateRepository.Create(problemInvestigate);
            unitOfWork.Save();
            return problemInvestigate.Id.ToString();
        }
    }


}
