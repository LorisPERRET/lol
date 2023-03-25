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
        private string _baseUri;

        public IChampionsManager ChampionsMgr { get; }

        public ISkinsManager SkinsMgr { get; }

        public IRunesManager RunesMgr { get; }

        public IRunePagesManager RunePagesMgr { get; }

        public API()
        {
            _httpClient = new HttpClient();

            //localhost
            //_httpClient.BaseAddress = new Uri("https://localhost:7185");
            //_baseUri = "/api/v2";

            //Codefirst Runner
            _httpClient.BaseAddress = new Uri("https://codefirst.iut.uca.fr");
            _baseUri = "/containers/hugolivet-LolAPI/api/v2";

            ChampionsMgr = new APIChampions(_httpClient, _baseUri);
            SkinsMgr = new APISkins(_httpClient, _baseUri);
            RunesMgr = new APIRunes(_httpClient, _baseUri);
            RunePagesMgr = new APIRunePages(_httpClient, _baseUri);
        }

    }
}
