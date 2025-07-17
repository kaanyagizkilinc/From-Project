using System.Security.Cryptography;
using System.Text;

namespace Modasaydasi.WebApi.Helper
{
    public static class Hash_SaltHelper
    {
        public static string HelperSalt(int size = 16)
        {
            var salt = new byte[size];
            using(var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HelperHash(string salt, string pass)
        {
            using (var sha256 = SHA256.Create())
            {
                var kombin = Encoding.UTF8.GetBytes(pass+ salt);
                var hash = sha256.ComputeHash(kombin);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
