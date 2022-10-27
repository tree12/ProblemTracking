using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Entity.Entities
{
    public class InvestigateStep: EntityObject<int>
    {
        public string StepName { get; set; }
        public string StepDetail { get; set; }
        public int Order { get; set; }

        public int MachineId { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; }

    }
}
