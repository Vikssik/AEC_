using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

    namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
    {
        public class UserRepository : BaseRepository<User>, IUserRepository
        {
            private readonly UserContext _context;

            public UserRepository(UserContext context) : base(context)
            {
                _context = context;
            }

            public void Create(User item)
            {
                base.Create(item);
            }

            public void Delete(int id)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }

            public void Update(User item)
            {
                var user = _context.Users.Find(item.UserID);
                if (user != null)
                {
                    user.Username = item.Username;
                    user.PasswordHash = item.PasswordHash;
                    user.Email = item.Email;
                    user.StatusUser = item.StatusUser;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
            }

            public User Get(int id)
            {
                return base.Get(id);
            }

            public IEnumerable<User> GetAll()
            {
                return base.GetAll();
            }

            public IEnumerable<User> Find(Func<User, bool> predicate)
            {
                return base.Find(predicate);
            }

            public int UserID
            {
                get => _context.Users.FirstOrDefault()?.UserID ?? 0;
                set
                {
                    var user = _context.Users.FirstOrDefault();
                    if (user != null)
                        user.UserID = value;
                }
            }

            public string Username
            {
                get => _context.Users.FirstOrDefault()?.Username;
                set
                {
                    var user = _context.Users.FirstOrDefault();
                    if (user != null)
                        user.Username = value;
                }
            }

            public string PasswordHash
            {
                get => _context.Users.FirstOrDefault()?.PasswordHash;
                set
                {
                    var user = _context.Users.FirstOrDefault();
                    if (user != null)
                        user.PasswordHash = value;
                }
            }

            public string Email
            {
                get => _context.Users.FirstOrDefault()?.Email;
                set
                {
                    var user = _context.Users.FirstOrDefault();
                    if (user != null)
                        user.Email = value;
                }
            }

            public string StatusUser
            {
                get => _context.Users.FirstOrDefault()?.StatusUser;
                set
                {
                    var user = _context.Users.FirstOrDefault();
                    if (user != null)
                        user.StatusUser = value;
                }
            }
        }
    }

