using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly UserContext _userContext;
        private readonly PermissionContext _permissionContext;
        private readonly ApplicationDbContext _context;

        private IUserRepository _userRepository;
        private IPermissionRepository _permissionRepository;
        private IOperationRepository _operationRepository;

        public EFUnitOfWork(DbContextOptions<UserContext> userContextOptions,
                            DbContextOptions<PermissionContext> permissionContextOptions,
                            DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            _userContext = new UserContext(userContextOptions);
            _permissionContext = new PermissionContext(permissionContextOptions);
            _context = new ApplicationDbContext(dbContextOptions);
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_userContext);
                return _userRepository;
            }
        }

        public IPermissionRepository Permissions
        {
            get
            {
                if (_permissionRepository == null)
                    _permissionRepository = new PermissionRepository(_permissionContext);
                return _permissionRepository;
            }
        }

        public IOperationRepository Operations
        {
            get
            {
                if (_operationRepository == null)
                    _operationRepository = new OperationRepository(_context);
                return _operationRepository;
            }
        }

        public void Save()
        {
            _userContext.SaveChanges();
            _permissionContext.SaveChanges();
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _userContext.Dispose();
                    _permissionContext.Dispose();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
