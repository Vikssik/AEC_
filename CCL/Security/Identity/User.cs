using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLL.Security.Identity
{
    public abstract class User { 

        public User (int userId, string name, string role)
        {
            UserId = userId;
            Name = name;
            Role = role;
        }

        public int UserId { get; }
        public string Name { get; }
        public string Role { get; }

       
        public abstract bool HasAccess(string functionality);
    }
}
