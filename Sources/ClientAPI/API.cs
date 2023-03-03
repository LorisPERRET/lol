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
        private HttpClient _httpClient;

        public IChampionsManager ChampionsMgr { get; }

        public ISkinsManager SkinsMgr { get; }

        public IRunesManager RunesMgr { get; }

        public IRunePagesManager RunePagesMgr { get; }

        public API()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7185");

            ChampionsMgr = new APIChampions(_httpClient);
            SkinsMgr = new APISkins(_httpClient);
            RunesMgr = new APIRunes(_httpClient);
            RunePagesMgr = new APIRunePages(_httpClient);
        }

    }
}
