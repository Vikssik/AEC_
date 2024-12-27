using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public class PermissionContext : DbContext
    {
        public PermissionContext(DbContextOptions<PermissionContext> options) : base(options)
        {
        }

        public virtual DbSet<Permission> Permissions { get; set; }

        public DbSet<Permission> Role { get; set; }


    }
}
