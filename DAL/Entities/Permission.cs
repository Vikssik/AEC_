using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string Role { get; set; }
    }
}
