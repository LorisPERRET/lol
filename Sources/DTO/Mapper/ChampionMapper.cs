using Model;

namespace DTO_API.Mapper
{
    public static class ChampionMapper
    {
        public static ChampionDto ToDto(this Champion champion)
        {
            return new ChampionDto
            {
                Name = champion.Name,
                Bio = champion.Bio,
                Class = champion.Class.ToString(),
                Icon = champion.Icon
            };
        }

        public static IEnumerable<ChampionDto> ToDtos(this IEnumerable<Champion> champions)
        {
            return champions.Select(c => ToDto(c));
        }

        public static Champion ToChampion(this ChampionDto champion)
        {
            return new Champion(champion.Name, Enum.Parse<ChampionClass>(champion.Class), champion.Icon, champion.Bio);
        }

        public static IEnumerable<Champion> ToChampions(this IEnumerable<ChampionDto> champions)
        {
            IList<Champion> liste = new List<Champion>();
            foreach (var champion in champions)
            {
                liste.Add(ToChampion(champion));
            }
            return liste;
        }
    }
}
