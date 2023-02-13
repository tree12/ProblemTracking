using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProblemTracking.Entity
{
    public class UserData
    {

        public static UserData SystemUserData => new UserData("system", "system");
        public static UserData AnonymousUserData => new UserData("anonymous", "anonymous");

        public UserData(string id, string name, List<Claim> claims = null)
        {
            Id = id;
            Name = name;
            Claims = claims;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public List<Claim> Claims { get; set; }
        public List<string> Roles
        {
            get
            {
                return Claims.Where(w => w.Type == ClaimTypes.Role).Select(claim => claim.Value).Distinct().ToList();
            }
        }
    }
}
