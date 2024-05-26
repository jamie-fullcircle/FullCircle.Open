using System.Data;
using Microsoft.Data.SqlClient;
using WeCare.api.repository;
using WeCare.api.self.types;

namespace WeCare.infra.repository;

public class SqlRepo : IReadOnlyRepoAsync
{
    private static readonly string CONNECTION_STRING =
        "Server=tcp:prod-core-sql.database.windows.net,1433;Initial Catalog=ProdWeCare;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";

    public async Task<List<LetterGrade>> ListLetterGradesAsync(EnLangTag langTag)
    {
        var results = new List<LetterGrade>();

        using var connection = new SqlConnection(CONNECTION_STRING);
        using var command = new SqlCommand("select id, name, assertion, bracket_low, bracket_high from letter_grade",
                                           connection);
        using var adapter = new SqlDataAdapter(command);
        using (var table = new DataTable())
        {
            await connection.OpenAsync();
            adapter.Fill(table);
            await connection.CloseAsync();

            foreach (DataRow row in table.Rows) results.Add(row.DeserializeLetterGrade(langTag));
        }

        return results;
    }
}

public static class DataRowExtensions
{
    public static LetterGrade DeserializeLetterGrade(this DataRow row, EnLangTag langTag)
    {
        var bilingualName = row["name"].ToString();
        if (bilingualName == null) throw new DataException("Invalid data, `name` cannot be null.");

        var bilingualAssertion = row["assertion"].ToString();
        if (bilingualAssertion == null) throw new DataException("Invalid data, `assertion` cannot be null.");

        var contentIndex = langTag == EnLangTag.En ? 0 : 1;

        return new LetterGrade(bilingualName.Split(["|"], StringSplitOptions.TrimEntries)[contentIndex],
                               bilingualAssertion.Split(new[] { "|" }, StringSplitOptions.TrimEntries)[contentIndex],
                               new Tuple<int?, int?>(
                                                     row["bracket_low"].ToString().TryParseNullable(),
                                                     row["bracket_high"].ToString().TryParseNullable()));
    }

    private static int? TryParseNullable(this string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;
        if (!int.TryParse(raw, out var parsed)) return null;
        return parsed;
    }
}