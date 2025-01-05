using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOperationService
    {
        bool CheckOperationAccess(int userId, string operationName);
        IEnumerable<OperationDTO> GetOperationsByRole(string roleName);
    }
}
