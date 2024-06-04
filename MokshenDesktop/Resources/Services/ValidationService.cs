using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MokshenDesktop.Resources.Services
{
    public static class ValidationService
    {
        public static bool ValidatePassword(string password) => password.Length > 10;
        public static bool ValidateLogin(string login) => login.Length > 1;
        public static bool ValidateEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        public static bool IsEmpty(string parameter) => string.IsNullOrEmpty(parameter);
        public static bool IsAnyEmpty(string login, string password) => string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password);
        public static bool IsAnyEmpty(string login, string password, string email) => string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email);
    }
}
