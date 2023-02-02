using DTO_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI
{
    public class ClientAPI
    {
        private HttpClient _httpClient;

        public ClientAPI()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7185");
        }

        public async Task<IEnumerable<ChampionDto>> GetChampions() => 
            await _httpClient.GetFromJsonAsync<IEnumerable<ChampionDto>>("/api/champions");

        public async Task<ChampionDto> GetChampionByName(string nom) =>
            await _httpClient.GetFromJsonAsync<ChampionDto>($"/api/Champions/{nom}");

        public async Task<HttpResponseMessage> DeleteChampion(string nom) => 
            await _httpClient.DeleteAsync($"/api/Champions/{nom}");
    }
}
