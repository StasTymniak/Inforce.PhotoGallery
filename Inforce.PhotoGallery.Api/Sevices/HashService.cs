using Inforce.PhotoGallery.Api.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Inforce.PhotoGallery.Api.Sevices
{
    public class HashService : IHashService
    {
        public async Task<string> HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
