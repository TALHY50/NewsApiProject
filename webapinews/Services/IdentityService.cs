using IdentityModel;
using System.Security.Principal;
using webapinews.Entities;
using webapinews.Models;

namespace webapinews.Services
{

    public class IdentityService
    {

        public Identity GetIdentity(HttpContext context)
        {

            var user = context.Items["User"] as User;
            if (user == null)
            {
                return null;
            }

            return new Identity { Id = user.Id };
        }


        public class Identity
        {
            public int Id { get; set; }
        }

    }




}
