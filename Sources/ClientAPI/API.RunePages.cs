using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public class APIRunePages : IRunePagesManager
    {
        public HttpClient _httpClient { get; }
        public string _baseUri { get; }

        public APIRunePages(HttpClient httpClient, string baseUri)
        {
            _httpClient = httpClient;
            _baseUri = baseUri;
        }

        public Task<RunePage?> AddItem(RunePage? item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItem(RunePage? item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunePage?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunePage?>> GetItemsByChampion(Champion? champion, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunePage?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunePage?>> GetItemsByRune(Model.Rune? rune, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItems()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByChampion(Champion? champion)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByName(string substring)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByRune(Model.Rune? rune)
        {
            throw new NotImplementedException();
        }

        public Task<RunePage?> UpdateItem(RunePage? oldItem, RunePage? newItem)
        {
            throw new NotImplementedException();
        }
    }
}
