using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskMain.Services
{
    public static class UserSession
    {
        public static string LoggedInUserEmail { get; private set; }

        public static void SetLoggedInUser(string userEmail)
        {
            LoggedInUserEmail = userEmail;
        }

        public static void ClearLoggedInUser()
        {
            LoggedInUserEmail = null;
        }
    }
}
