using ProblemTracking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemTracking.Entity;
using ProblemTracking.Entity.Entities;

namespace ProblemTracking.Repository
{
    public class InvestigateStepRepository: RepositoryBase<InvestigateStep>
    {
        public InvestigateStepRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
