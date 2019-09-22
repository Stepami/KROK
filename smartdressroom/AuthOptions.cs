using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace smartdressroom
{
    public class AuthOptions
    {
        public const string ISSUER = "CROCMirror"; // издатель токена
        public const string AUDIENCE = "http://localhost:5123/"; // потребитель токена
        const string KEY = "API_AUTH_t0k3n_k3y!";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - 5 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}