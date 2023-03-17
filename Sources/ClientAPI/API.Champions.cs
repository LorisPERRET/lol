using DTO_API;
using DTO_API.Mapper;
using DTO_API.Pagination;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            int page = index / count;
            string query = String.Format($"/Champions?page={0}&offset={1}&championClass={2}&orderingPropertyName={3}&descending={4}",page,count,null,orderingPropertyName,descending);
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>(query);
            return res.Items.ToChampions();
        }

        public Task<IEnumerable<Champion?>> GetItemsByCharacteristic(string charName, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Champion?>> GetItemsByClass(ChampionClass championClass, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            int page = index / count;
            string query = String.Format($"/Champions?page={0}&offset={1}&championClass={2}&orderingPropertyName={3}&descending={4}", page, count, championClass.ToString(), orderingPropertyName, descending);
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>(query);
            return res.Items.ToChampions();
        }

        public async Task<IEnumerable<Champion?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            string query = String.Format($"/Champions/{0}",substring);
            var res = await _httpClient.GetFromJsonAsync<ChampionDto>(query);
            return new List<Champion> { res.ToChampion() };
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

        public async Task<int> GetNbItems()
        { 
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>($"/Champions");
            return res.NbItem;
        }

        public Task<int> GetNbItemsByCharacteristic(string charName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNbItemsByClass(ChampionClass championClass)
        {
            string query = String.Format($"/Champions?championClass={0}", championClass.ToString());
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>(query);
            return res.NbItem;
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            string query = String.Format($"/Champions/{0}", substring);
            var res = await _httpClient.GetFromJsonAsync<ChampionDto>(query);
            return res is null ? 0 : 1;
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
