using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Entity.Entities.Interfaces
{
    public interface IEntityObjectBase
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(36)]
        public string? CreatedUserId { get; set; }

        [MaxLength(36)]
        public string? ModifiedUserId { get; set; }
    }
}
