using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Model;

namespace ClientDb
{
    public class BDD : IDataManager
    {
        private DbContext _dbContext;

        public IChampionsManager ChampionsMgr { get; }

        public ISkinsManager SkinsMgr { get; }

        public IRunesManager RunesMgr { get; }

        public IRunePagesManager RunePagesMgr { get; }

        public BDD(DbContext dbContext)
        {
            _dbContext = dbContext;

            ChampionsMgr = new BDDChampions(_dbContext);
            SkinsMgr = new BDDSkins(_dbContext);
            RunesMgr = new BDDRunes(_dbContext);
            RunePagesMgr = new BDDRunePages(_dbContext);
        }
    }
}