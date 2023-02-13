using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProblemTracking.Entity;
using ProblemTracking.Entity.Entities;
using ProblemTracking.Repository;
using ProblemTracking.Web.Model;
using System.Linq;
using Mapster;

namespace ProblemTracking.Web.Services
{
    public class UserService : BaseService<UserViewModel, string>
    {
        private IConfiguration _configuration { get; }
        public UserService(UnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            _configuration = configuration;
        }
        public UserViewModel AuthenUser(UserViewModel loginCredentials)
        {
            var user = unitOfWork.UserRepository.FindByCondition(x =>
                x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password).FirstOrDefault();

            return user.Adapt<UserViewModel>();
        }
    }
}
