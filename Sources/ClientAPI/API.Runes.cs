using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public class APIRunes : IRunesManager
    {
        public HttpClient _httpClient { get; }
        public string _baseUri { get; }

        public APIRunes(HttpClient httpClient, string baseUri)
        {
            _httpClient = httpClient;
            _baseUri = baseUri;
        }

        public Task<Model.Rune?> AddItem(Model.Rune? item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItem(Model.Rune? item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model.Rune?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model.Rune?>> GetItemsByFamily(RuneFamily family, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model.Rune?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItems()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByFamily(RuneFamily family)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNbItemsByName(string substring)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Rune?> UpdateItem(Model.Rune? oldItem, Model.Rune? newItem)
        {
            throw new NotImplementedException();
        }
    }
}
