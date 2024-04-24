using System.Security.Cryptography;
using System.Text;

namespace PryEcommerce.Infraestructura.Util.Encriptar;

public class Encriptacion
{
    public static string EncriptarClave(string password)
    {
        using (SHA256 hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(password));
        
            StringBuilder sb = new StringBuilder();
        
            foreach (byte b in result)
            {
                sb.AppendFormat("{0:x2}", b);
            }
        
            return sb.ToString();
        }
    }
}