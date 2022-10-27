using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Entity.Entities
{
    public class Problem : EntityObject<int>
    {
        public string ProblemName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public User User { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Problem problem &&
                   ProblemName == problem.ProblemName;
        }
    }
}
