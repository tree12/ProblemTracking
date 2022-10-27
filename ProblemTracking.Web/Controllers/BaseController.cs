using Microsoft.AspNetCore.Mvc;
using ProblemTracking.Service.Services;

namespace ProblemTracking.Web.Controllers
{
    public class BaseController<TService> : Controller where TService : class
    {
        protected TService Service { get; set; }
        public BaseController(IServiceFactory serviceFactory)
        {
            this.Service = serviceFactory.GetService<TService>();
        }
        public IActionResult Index()
        {
            return View();
        }

    }

}
