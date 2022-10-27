using ProblemTracking.Entity;

namespace ProblemTracking.Service.Services
{
    public abstract class BaseService<TViewModel,TType> where TViewModel : Model.BaseViewModel<TType>
    {
        protected ApplicationDbContext coreDbContext;
        //protected readonly ILogger<BaseService<TViewModel>> logger;
        public BaseService(ApplicationDbContext coreDbContext)
        {
            this.coreDbContext = coreDbContext;
        }
        //public abstract Task<PaginatedListViewModel<TViewModel>> AsPagedListAsync(PageableInfo info);
    }
}
