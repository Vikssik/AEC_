using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public interface IOperationRepository : IRepository<Operation>
    {
        IEnumerable<Operation> FindByName(string name);
    }
}
