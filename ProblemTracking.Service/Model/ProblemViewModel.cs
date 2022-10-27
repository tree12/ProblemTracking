using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemTracking.Entity.Enum;

namespace ProblemTracking.Service.Model
{
    public class ProblemViewModel: BaseViewModel<int>
    {
        public string ProblemName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public UserViewModel User { get; set; }

        public SolveEnum SolveStatus { get; set; }
        public string SuceesInvestigateName { get; set; }
    }
}
