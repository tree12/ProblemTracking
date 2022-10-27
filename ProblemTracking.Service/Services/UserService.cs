using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProblemTracking.Entity;
using ProblemTracking.Entity.Entities;
using ProblemTracking.Service.Model;
using System.Linq;

namespace ProblemTracking.Service.Services
{
    public class UserService : BaseService<UserViewModel, string>
    {
        private IConfiguration _configuration { get; }
        public UserService(ApplicationDbContext coreDbContext, IConfiguration configuration) : base(coreDbContext)
        {
            _configuration = configuration;
        }
        public UserViewModel AuthenUser(UserViewModel loginCredentials)
        {
            var user = (from x in coreDbContext.User.Where(x=> x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password)
                        select new UserViewModel
                        {
                            UserName = x.UserName,
                            FirstName = x.FirstName,
                            Password = x.Password
                        }).FirstOrDefault();

            return user;
        }
    }
}
