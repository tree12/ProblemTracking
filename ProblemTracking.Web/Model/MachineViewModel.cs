using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Web.Model
{
    public class MachineViewModel : BaseViewModel<int>
    {
        public string MachineName { get; set; }
        public string MachineDescription { get; set; }
        public List<InvestigateStepViewModel> InvestigateSteps { get; set; }
    }
}
