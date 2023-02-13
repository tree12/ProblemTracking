using ProblemTracking.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Web.Model
{
    public class ProblemInvestigateViewModel : BaseViewModel<string>
    {
        public ProblemViewModel Problem { get; set; }
        public InvestigateStepViewModel StepToSolved { get; set; }
        public SolveEnum SolveStatus { get; set; }
        public byte[] Picture { get; set; }
    }
}
