using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLL.Security.Identity
{
    public class TechnicalSpecialist : User
    {
        public TechnicalSpecialist(int userId, string name)
             : base(userId, name, nameof(TechnicalSpecialist))
        {
        }

        public override bool HasAccess(string functionality)
        {
            return functionality == "UpdateDocumentation" || functionality == "OrganizeMaintenance";
        }
    }
}
