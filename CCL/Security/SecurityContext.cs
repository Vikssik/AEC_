using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace CLL.Security.Identity
{
    public static class SecurityContext
    {
        private static User _currentUser;

        public static User GetCurrentUser()
        {
            return _currentUser;
        }

        public static void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}
