using WeCare.api.self.types;

namespace WeCare.api.repository;

public interface IReadOnlyRepo
{
    public List<LetterGrade> ListLetterGrades(EnLangTag langTag);
}

public interface IReadOnlyRepoAsync
{
    public Task<List<LetterGrade>> ListLetterGradesAsync(EnLangTag langTag);
}