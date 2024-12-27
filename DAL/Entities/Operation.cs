using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public class Operation
    {
        public int OperationID { get; set; }
        public string OperationName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
