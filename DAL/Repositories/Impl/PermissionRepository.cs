using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        private readonly PermissionContext _context;

        public PermissionRepository(PermissionContext context)
            : base(context)
        {
            _context = context;
        }

        public void Create(Permission item)
        {
            base.Create(item);
        }

        public void Delete(int id)
        {
            base.Delete(id);
        }

        public void Update(Permission item)
        {
            base.Update(item);
        }

        public Permission Get(int id)
        {
            return base.Get(id);
        }

        public IEnumerable<Permission> GetAll()
        {
            return base.GetAll();
        }

        public IEnumerable<Permission> Find(Func<Permission, bool> predicate)
        {
            return base.Find(predicate);
        }

        public string GetRoleByPermissionID(int permissionId)
        {
            var permission = _context.Permissions.FirstOrDefault(p => p.PermissionID == permissionId);
            return permission?.Role;
        }

        public void UpdateRole(int permissionId, string newRole)
        {
            var permission = _context.Permissions.FirstOrDefault(p => p.PermissionID == permissionId);
            if (permission != null)
            {
                permission.Role = newRole;
                _context.SaveChanges();
            }
        }
        public IEnumerable<Permission> FindByRole(string roleName)
        {
            return _context.Permissions.Where(p => p.Role == roleName).ToList();
        }

    }
}