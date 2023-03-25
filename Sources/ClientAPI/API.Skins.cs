using DTO_API.Mapper;
using DTO_API;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DTO_API.Pagination;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClientAPI
{
    public class APISkins : ISkinsManager
    {
        public HttpClient _httpClient { get; }
        public string _baseUri { get; }

        public APISkins(HttpClient httpClient, string baseUri)
        {
            _httpClient = httpClient;
            _baseUri = baseUri;
        }

        public async Task<Skin?> AddItem(Skin? item)
        {
            var res = await _httpClient.PostAsJsonAsync<SkinDto>($"{_baseUri}/Skins", item.ToDto());
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return (await res.Content.ReadFromJsonAsync<SkinDto>()).ToSkin();
            }
            else throw new HttpRequestException();
        }

        public async Task<bool> DeleteItem(Skin? item)
        {
            var res = await _httpClient.DeleteAsync($"{_baseUri}/Skins/{item.Name}");
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else return false;
        }

        public async Task<IEnumerable<Skin?>> GetItems(int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"{_baseUri}/Skins?page={index}&offset={count}&orderingPropertyName={orderingPropertyName}&descending={descending}");
            return res.Items.ToSkins();
        }

        public async Task<IEnumerable<Skin?>> GetItemsByChampion(Champion? champion, int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"{_baseUri}/Champions/{champion.Name}/skins?page={index}&offset={count}&orderingPropertyName={orderingPropertyName}&descending={descending}");
            return res.Items.ToSkins();
        }

        // Etant donné que le nom d'un skin est unique : cette méthode est utilisé pour récupérer une seul skin et son image
        public async Task<IEnumerable<Skin?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<SkinDto>($"{_baseUri}/Skins/{substring}");
            var skin = res.ToSkin();

            // Récupération de l'image séparée afin de réduire le coût
            var image = await _httpClient.GetFromJsonAsync<LargeImageDto>($"{_baseUri}/Skins/{substring}/image");
            skin.Image = image.ToLargeImage();
            return new List<Skin> { skin };
        }

        public async Task<int> GetNbItems()
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"{_baseUri}/Skins");
            return res.NbItem;
        }

        public async Task<int> GetNbItemsByChampion(Champion? champion)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"{_baseUri}/Champions/{champion.Name}/skins");
            return res.NbItem;
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            var res = await _httpClient.GetFromJsonAsync<SkinDto>($"{_baseUri}/Skin/{substring}");
            return res is null ? 0 : 1;
        }

        public async Task<Skin?> UpdateItem(Skin? oldItem, Skin? newItem)
        {
            var res = await _httpClient.PutAsJsonAsync<SkinDto>($"{_baseUri}/Skins/{oldItem.Name}", newItem.ToDto());
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return (await res.Content.ReadFromJsonAsync<SkinDto>()).ToSkin();
            }
            else throw new HttpRequestException();
        }
    }
}
