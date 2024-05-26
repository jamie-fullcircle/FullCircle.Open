using System.Diagnostics;
using System.Reflection;

namespace WeCare.infra.http;

public class Server
{
    private readonly WebApplication _app;
    private HashSet<WebApi> _runningApis;

    public Server()
    {
        _runningApis = new HashSet<WebApi>();
        _app = Configure();
    }

    private WebApplication Configure()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddSwaggerGen()
               .AddAuthorization()
               .AddEndpointsApiExplorer();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        ConfigureServices(app);

        return app;
    }

    private void ConfigureServices(WebApplication app)
    {
        var apis = new List<WebApi>();
        foreach (var type in
                 Assembly.GetAssembly(typeof(WebApi))?.GetTypes()
                         .Where(maybeType => maybeType is { IsClass: true, IsAbstract: false } &&
                                             maybeType.IsSubclassOf(typeof(WebApi)))!)
            apis.Add((WebApi)Activator.CreateInstance(type)!);

        apis.ForEach(api =>
                     {
                         Debug.WriteLine(api.GetType());
                         api.Configure(app);
                     });
    }

    public void Run()
    {
        _app.Run();
    }
}