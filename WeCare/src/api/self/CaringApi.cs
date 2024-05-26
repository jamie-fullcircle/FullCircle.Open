using WeCare.api.repository;
using WeCare.api.self.types;
using WeCare.infra.repository;

namespace WeCare.api.self;

public class CaringApi
{
    private readonly IReadOnlyRepo _repo;

    public CaringApi()
    {
        _repo = new ConstantRepo();
    }

    public List<LetterGrade> GetAllLetterGrades(EnLangTag langTag)
    {
        return _repo.ListLetterGrades(langTag);
    }
}