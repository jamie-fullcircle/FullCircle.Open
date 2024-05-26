using WeCare.api.self;
using WeCare.api.self.types;

namespace WeCare.infra.http;

public class LocationApi : WebApi
{
    private readonly CaringApi _caringApi;

    public LocationApi()
    {
        _caringApi = new CaringApi();
    }

    public override void Configure(WebApplication app)
    {
        app.MapGet("{langTag}/grade/", (EnLangTag langTag) => _caringApi.GetAllLetterGrades(langTag));
    }
}