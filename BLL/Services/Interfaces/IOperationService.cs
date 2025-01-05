using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOperationService
    {
        bool CheckOperationAccess(string operationName);
        IEnumerable<OperationDTO> GetOperationsByRole(string roleName);
    }
}
