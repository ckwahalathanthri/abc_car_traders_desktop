using ABCCarTraders.DataAccess;
using ABCCarTraders.Models;

namespace ABCCarTraders.Business
{
    public static class AuthenticationService
    {
        public static User CurrentUser { get; private set; }

        public static bool Login(string username, string password)
        {
            var userRepository = new UserRepository();
            var user = userRepository.GetUserByCredentials(username, password);
            
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            
            return false;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;
        public static bool IsAdmin => CurrentUser?.UserType == UserType.Admin;
        public static bool IsCustomer => CurrentUser?.UserType == UserType.Customer;
    }
}