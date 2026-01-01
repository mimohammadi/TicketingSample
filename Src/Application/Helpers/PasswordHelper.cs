using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher<object> _hasher = new();

        public static string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public static bool Verify(string hash, string password)
        {
            var result = _hasher.VerifyHashedPassword(
                null!, hash, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
