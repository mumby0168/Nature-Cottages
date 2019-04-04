using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NatureCottages.App;

namespace NatureCottages
{
    public class CurrentUser : IUser
    {
        public static string Username { get; set; }
        public int UserId { get; set; }
    }
}
