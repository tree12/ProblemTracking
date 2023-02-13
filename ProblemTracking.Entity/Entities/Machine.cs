using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Entity.Entities
{
    public class Machine : EntityObject<int> {
        public string MachineName { get; set; }
        public string MachineDescription { get; set; }
        public List<InvestigateStep> InvestigateSteps { get; set; }
    }
    
}
