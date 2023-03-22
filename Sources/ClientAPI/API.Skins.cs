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

        public APISkins(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Skin?> AddItem(Skin? item)
        {
            var res = await _httpClient.PostAsJsonAsync<SkinDto>("/api/Skins", item.ToDto());
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return (await res.Content.ReadFromJsonAsync<SkinDto>()).ToSkin();
            }
            else throw new HttpRequestException();
        }

        public async Task<bool> DeleteItem(Skin? item)
        {
            var res = await _httpClient.DeleteAsync($"/api/Skins/{item.Name}");
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else return false;
        }

        public async Task<IEnumerable<Skin?>> GetItems(int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"/api/Skins?page={index}&offset={count}&orderingPropertyName={orderingPropertyName}&descending={descending}");
            return res.Items.ToSkins();
        }

        public async Task<IEnumerable<Skin?>> GetItemsByChampion(Champion? champion, int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"/api/Champions/{champion.Name}/skins?page={index}&offset={count}&orderingPropertyName={orderingPropertyName}&descending={descending}");
            return res.Items.ToSkins();
        }

        // Etant donné que le nom d'un skin est unique : cette méthode est utilisé pour récupérer une seul skin et son image
        public async Task<IEnumerable<Skin?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = "", bool descending = false)
        {
            var res = await _httpClient.GetFromJsonAsync<SkinDto>($"/api/Skins/{substring}");
            var skin = res.ToSkin();

            // Récupération de l'image séparée afin de réduire le coût
            var image = await _httpClient.GetFromJsonAsync<LargeImageDto>($"/api/Skins/{substring}/image");
            skin.Image = image.ToLargeImage();
            return new List<Skin> { skin };
        }

        public async Task<int> GetNbItems()
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"/api/Skins");
            return res.NbItem;
        }

        public async Task<int> GetNbItemsByChampion(Champion? champion)
        {
            var res = await _httpClient.GetFromJsonAsync<Page<IEnumerable<SkinDto>>>($"/api/Champions/{champion.Name}/skins");
            return res.NbItem;
        }

        public async Task<int> GetNbItemsByName(string substring)
        {
            var res = await _httpClient.GetFromJsonAsync<SkinDto>($"/api/Skin/{substring}");
            return res is null ? 0 : 1;
        }

        public async Task<Skin?> UpdateItem(Skin? oldItem, Skin? newItem)
        {
            var res = await _httpClient.PutAsJsonAsync<SkinDto>($"/api/Skins/{oldItem.Name}", newItem.ToDto());
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return (await res.Content.ReadFromJsonAsync<SkinDto>()).ToSkin();
            }
            else throw new HttpRequestException();
        }
    }
}
