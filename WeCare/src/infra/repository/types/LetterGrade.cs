namespace WeCare.infra.repository.types;

public record LetterGrade(BilingualDatum Name, BilingualDatum Assertion, Tuple<int?, int?> Bracket);