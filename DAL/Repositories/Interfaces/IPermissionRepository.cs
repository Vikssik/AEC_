using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        string GetRoleByPermissionID(int permissionId);


        void UpdateRole(int permissionId, string newRole);


        IEnumerable<Permission> FindByRole(string roleName);
    }
}
