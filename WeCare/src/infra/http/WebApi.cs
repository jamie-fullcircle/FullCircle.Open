namespace WeCare.infra.http;

public class RootApi : WebApi
{
    public override void Configure(WebApplication app)
    {
        app.MapGet("api/", () => "Hello, World!");
    }
}

public abstract class WebApi
{
    public abstract void Configure(WebApplication app);
}