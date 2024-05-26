using WeCare.infra.http;

namespace WeCare.infra;

public class Bootstrap
{
    public static void Main(string[] args)
    {
        var server = new Server();
        server.Run();
    }
}