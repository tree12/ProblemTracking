using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTracking.Service.Model
{
    public class UserViewModel: BaseViewModel<string> 
    {
        public static Dictionary<string, string> UserTypes = new Dictionary<string, string> { { "User1", "Admin" }, { "User2", "User" }, { "User3", "User" } };
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role => UserTypes[UserName];
   
    }



}
