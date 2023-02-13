using ProblemTracking.Entity;
using ProblemTracking.Repository;

namespace ProblemTracking.Web.Services
{
    public abstract class BaseService<TViewModel,TType> where TViewModel : Model.BaseViewModel<TType>
    {
        protected UnitOfWork unitOfWork;
        public BaseService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

    }
}
