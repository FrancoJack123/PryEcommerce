namespace PryEcommerce.Infraestructura.Util.Token;

public class Token
{
    public static string GenerarToken()
    {
        string token = Guid.NewGuid().ToString("N");
        return token;
    }
}