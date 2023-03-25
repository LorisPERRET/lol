using DTO_API;
using DTO_API.Mapper;
using DTO_API.Pagination;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public string _baseUri { get; }
        public APIChampions(HttpClient httpClient, string baseUri)
        {
            _httpClient = httpClient;
            _baseUri = baseUri;
        }

        public async Task<Champion?> AddItem(Champion? item)
        {
            var res = await _httpClient.PostAsJsonAsync<ChampionDto>($"{_baseUri}/Champions", item.ToDto());
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return (await res.Content.ReadFromJsonAsync<ChampionDto>()).ToChampion();
            }
            else throw new HttpRequestException();
        }

        public async Task<bool> DeleteItem(Champion? item)
        {
            var res = await _httpClient.DeleteAsync($"{_baseUri}/Champions/{item.Name}");
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else return false;
        }

        public async Task<IEnumerable<Champion?>> GetItems(int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>($"{_baseUri}/Champions?page={index}&offset={count}&orderingPropertyName={orderingPropertyName}&descending={descending}");
            return res.Items.ToChampions();
        }

        public Task<IEnumerable<Champion?>> GetItemsByCharacteristic(string charName, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Champion?>> GetItemsByClass(ChampionClass championClass, int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>($"{_baseUri}/Champions?page={index}&offset={count}&championClass={championClass}&orderingPropertyName={orderingPropertyName}&descending={descending}");
            return res.Items.ToChampions();
        }

        // Etant donné que le nom d'un champion est unique : cette méthode est utilisé pour récupérer une seul champion et son image
        public async Task<IEnumerable<Champion?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<ChampionDto>($"{_baseUri}/Champions/{substring}");
            var champion = res.ToChampion();

            // Récupération de l'image séparée afin de réduire le coût
            var image = await _httpClient.GetFromJsonAsync<LargeImageDto>($"{_baseUri}/Champions/{substring}/image");
            champion.Image = image.ToLargeImage();

            return new List<Champion> { champion };
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
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>($"{_baseUri}/Champions");
            return res.NbItem;
        }

        public Task<int> GetNbItemsByCharacteristic(string charName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNbItemsByClass(ChampionClass championClass)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<ChampionDto>>>($"{_baseUri}/Champions?championClass={championClass}");
            return res.NbItem;
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            var res = await _httpClient.GetFromJsonAsync<ChampionDto>($"{_baseUri}/Champions/{substring}");
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

        public async Task<Champion?> UpdateItem(Champion? oldItem, Champion? newItem)
        {
            var res = await _httpClient.PutAsJsonAsync<ChampionDto>($"{_baseUri}/Champions/{oldItem.Name}", newItem.ToDto());
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return (await res.Content.ReadFromJsonAsync<ChampionDto>()).ToChampion();
            }
            else throw new HttpRequestException();
        }
    }


}
