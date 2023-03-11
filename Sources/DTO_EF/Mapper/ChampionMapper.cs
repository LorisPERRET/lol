using Model;

namespace DTO_EF.Mapper
{
    public static class ChampionMapper
    {

        public static ChampionEntity ToEntity(this Champion champion)
        {
            return new ChampionEntity
            {
                Name = champion.Name,
                Bio = champion.Bio,
                Class = champion.Class.ToString(),
                Icon = champion.Icon,
                Image = champion.Image.Base64,
            };
        }

        public static IEnumerable<ChampionEntity> ToEntities(this IEnumerable<Champion> champions)
        {
            return champions.Select(c => ToEntity(c));
        }


        public static Champion ToChampion(this ChampionEntity champion)
        {
            return new Champion(champion.Name, Enum.Parse<ChampionClass>(champion.Class), champion.Icon, champion.Image, champion.Bio);
        }

        public static IEnumerable<Champion> ToChampions(this IEnumerable<ChampionEntity> champions)
        {
            return champions.Select(c => ToChampion(c));
        }


    }
}
