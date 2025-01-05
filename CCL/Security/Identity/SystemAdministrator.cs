using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLL.Security.Identity
{
    public class SystemAdministrator : User
    {
        public SystemAdministrator(int userId, string name)
            : base(userId, name, nameof(SystemAdministrator))
        {
        }

        public override bool HasAccess(string functionality)
        {
            
            return functionality == "ManageUsers" || functionality == "ConfigureAccess";
        }
    }
}
