using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Web.Model
{
    public class InvestigateStepViewModel: BaseViewModel<int>
    {
        public string StepName { get; set; }
        public string StepDetail { get; set; }
        public int Order { get; set; }

        public int MachineId { get; set; }

        public MachineViewModel Machine { get; set; }
    }
}
