using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        int UserID { get; set; }
        string Username { get; set; }
        string PasswordHash { get; set; }
        string Email { get; set; }
        string StatusUser { get; set; }
    }
}
