using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NatureCottages.Attributes
{
    public class UserLoggedInAuth : IAuthorizationFilter
    {        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
        }
    }
}
