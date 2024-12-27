using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IUserRepository Users { get; }

        IOperationRepository Operations { get; }
        void Save();
    }
}