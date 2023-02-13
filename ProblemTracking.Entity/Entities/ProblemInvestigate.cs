using ProblemTracking.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Entity.Entities
{
    public class ProblemInvestigate : EntityObject<Guid>
    {
        public Problem Problem { get; set; }
        public InvestigateStep StepToSolved { get; set; }
        public SolveEnum SolveStatus { get; set; }
        public byte[] Picture { get; set; }
    }
}
