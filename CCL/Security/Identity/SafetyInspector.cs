using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLL.Security.Identity
{
    public class SafetyInspector : User
    {
        public SafetyInspector(int userId, string name)
            : base(userId, name, nameof(SafetyInspector))
        {
        }

        public override bool HasAccess(string functionality)
        {
           
            return functionality == "AuditSafety" || functionality == "ReceiveAlerts";
        }
    }
}
