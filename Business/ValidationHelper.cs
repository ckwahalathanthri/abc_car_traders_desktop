using System.Text.RegularExpressions;

namespace ABCCarTraders.Business
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            string pattern = @"^[\+]?[0-9\-\s\(\)]{7,15}$";
            return Regex.IsMatch(phone, pattern);
        }

        public static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return false;

            // At least one letter and one number
            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).{6,}$");
        }

        public static string ValidateUser(string username, string password, string email, string fullName)
        {
            if (string.IsNullOrWhiteSpace(username))
                return "Username is required.";

            if (username.Length < 3)
                return "Username must be at least 3 characters long.";

            if (!IsStrongPassword(password))
                return "Password must be at least 6 characters and contain letters and numbers.";

            if (!IsValidEmail(email))
                return "Please enter a valid email address.";

            if (string.IsNullOrWhiteSpace(fullName))
                return "Full name is required.";

            return null; // No errors
        }
    }
}