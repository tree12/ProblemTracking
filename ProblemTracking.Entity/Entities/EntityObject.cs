using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ProblemTracking.Entity.Entities.Interfaces;


namespace ProblemTracking.Entity.Entities
{
    public abstract class EntityObject<IdType> : IEntityObject<IdType>
    {
        public virtual IdType Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(36)]
        public string? CreatedUserId { get; set; }

        [MaxLength(36)]
        public string? ModifiedUserId { get; set; }


       
    }
}
