using DTO_API;
using DTO_API.Mapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public class APIChampions : IChampionsManager
    {
        public HttpClient _httpClient { get; }
        public APIChampions(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Champion?> AddItem(Champion? item)
        {
            var res = await _httpClient.PostAsJsonAsync<ChampionDto>("/Champions", item.ToDto());
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return (await res.Content.ReadFromJsonAsync<ChampionDto>()).ToChampion();
            }
            else throw new HttpRequestException();
        }

        public async Task<bool> DeleteItem(Champion? item)
        {
            var res = await _httpClient.DeleteAsync($"/Champions/{item.Name}");
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else return false;
        }

        public async Task<IEnumerable<Champion?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            //var res = await _httpClient.GetAsync($"");
            throw new NotImplementedException();

        }

        public Task<IEnumerable<Champion?>> GetItemsByCharacteristic(string charName, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion?>> GetItemsByClass(ChampionClass championClass, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion?>> GetItemsByRunePage(RunePage? runePage, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion?>> GetItemsBySkill(Skill? skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Champion?>> GetItemsBySkill(string skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItems()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByCharacteristic(string charName)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByClass(ChampionClass championClass)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByName(string substring)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByRunePage(RunePage? runePage)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsBySkill(Skill? skill)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsBySkill(string skill)
        {
            throw new NotImplementedException();
        }

        public Task<Champion?> UpdateItem(Champion? oldItem, Champion? newItem)
        {
            throw new NotImplementedException();
        }
    }


}
