using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemTracking.Entity;
using ProblemTracking.Entity.Entities;

namespace ProblemTracking.Repository
{
    public class UserRepository: RepositoryBase<User>
    {
        public UserRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
