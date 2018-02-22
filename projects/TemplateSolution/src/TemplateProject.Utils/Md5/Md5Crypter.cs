using System;
using System.Security.Cryptography;
using System.Text;

namespace TemplateProject.Utils.Md5
{
    public sealed class Md5Crypter : IMd5Crypter
    {
        public string Encrypt(string data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (var md5 = MD5.Create())
            {
                var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}