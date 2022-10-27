using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Service.Model
{
    public abstract class BaseViewModel<TType>
    {
        public TType Id { get; set; }
    }
}
