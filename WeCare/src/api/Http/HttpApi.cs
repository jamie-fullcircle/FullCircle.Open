using WeCare.api.self;
using WeCare.api.self.types;

namespace WeCare.api.Http;

public class HttpApi
{
    private readonly CaringApi _caringApi;

    public HttpApi()
    {
        _caringApi = new CaringApi();
    }

    public string Hello => "Hello, World!";

    public List<LetterGrade> ListGrades(EnLangTag langTag)
    {
        return _caringApi.GetAllLetterGrades(langTag);
    }
}