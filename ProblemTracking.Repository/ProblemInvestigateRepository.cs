using ProblemTracking.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemTracking.Entity;

namespace ProblemTracking.Repository
{
    public class ProblemInvestigateRepository: RepositoryBase<ProblemInvestigate>
    {
        public ProblemInvestigateRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
