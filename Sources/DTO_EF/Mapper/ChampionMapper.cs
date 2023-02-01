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
                Icon = champion.Icon
            };
        }

        public static IEnumerable<ChampionEntity> ToEntities(this IEnumerable<Champion> champions)
        {
            return champions.Select(c => ToEntity(c));
        }


    }
}
