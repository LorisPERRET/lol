using DTO;
using Model;

namespace API.Mapper
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


    }
}
