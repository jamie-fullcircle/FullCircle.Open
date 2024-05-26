using WeCare.api.repository;
using WeCare.api.self.types;
using WeCare.infra.repository.types;
using LetterGrade = WeCare.infra.repository.types.LetterGrade;
using Offering = WeCare.infra.repository.types.Offering;

namespace WeCare.infra.repository;

public class ConstantRepo : IReadOnlyRepo
{
    public List<api.self.types.LetterGrade> ListLetterGrades(EnLangTag langTag)
    {
        return Data.LetterGrades
                   .Select(lg =>
                               new api.self.types.LetterGrade(langTag == EnLangTag.En ? lg.Name.En : lg.Name.Fr,
                                                              langTag == EnLangTag.En
                                                                  ? lg.Assertion.En
                                                                  : lg.Assertion.Fr,
                                                              lg.Bracket))
                   .ToList();
    }

    private class Data
    {
        //general config
        public static Dictionary<string, BilingualDatum> Program = new()
                                                                   {
                                                                       {
                                                                           "programName",
                                                                           new BilingualDatum("we care",
                                                                                              "nous nous soucions")
                                                                       }
                                                                   };

        //letter grades for establishments
        public static readonly LetterGrade[] LetterGrades =
        {
            new(new BilingualDatum("the door", "la porte"),
                new BilingualDatum("we exist", "nous existons"),
                new Tuple<int?, int?>(null, 9)),
            new(new BilingualDatum("the lobby", "le loby"),
                new BilingualDatum("we care", "nous nous soucions"),
                new Tuple<int?, int?>(10, 19)),
            new(new BilingualDatum("the washroom", "la salle de bain"),
                new BilingualDatum("we understand", "nous comprenons"),
                new Tuple<int?, int?>(20, 29)),
            new(new BilingualDatum("the breakroom", "la salle de repos"),
                new BilingualDatum("we've got your back", "nous avons votre dos"),
                new Tuple<int?, int?>(30, 39)),
            new(new BilingualDatum("the keys", "les clés"),
                new BilingualDatum("it's your place", "c'est chez toi"),
                new Tuple<int?, int?>(40, null))
        };


        //things available to the public
        public static List<Offering> Offerings = new()
                                                 {
                                                     new Offering(1, 5,
                                                                  new BilingualDatum("washroom", "salle de bain")),
                                                     new Offering(1, 3,
                                                                  new BilingualDatum("washroom key",
                                                                                     "clé des toilettes")),
                                                     new Offering(1, 3,
                                                                  new BilingualDatum("washroom key",
                                                                                     "clé des toilettes"))
                                                 };

        //can't have more than one from each set
        public static List<Tuple<int, int>> MutualExclusiveOfferings = new()
                                                                       {
                                                                           new Tuple<int, int>(1, 2)
                                                                       };
    }
}