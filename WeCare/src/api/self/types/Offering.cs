namespace WeCare.api.self.types;

public record Offering(int Id, int Score, string Name);

public enum EnLangTag
{
    En,
    Fr
}