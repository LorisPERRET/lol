using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public class API : IDataManager
    {
        public IChampionsManager ChampionsMgr => throw new NotImplementedException();

        public ISkinsManager SkinsMgr => throw new NotImplementedException();

        public IRunesManager RunesMgr => throw new NotImplementedException();

        public IRunePagesManager RunePagesMgr => throw new NotImplementedException();
    }
}
