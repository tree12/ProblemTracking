using ProblemTracking.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemTracking.Entity;

namespace ProblemTracking.Repository
{
    public class ProblemRepository : RepositoryBase<Problem>
    {
        public ProblemRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
