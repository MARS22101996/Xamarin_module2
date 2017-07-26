using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Infrastructure;
using VTSClient.DAL.Interfaces;

namespace VTSClient.DAL.Repositories
{
    public class CommonApiRepository<TEntity> : IApiRepository<TEntity> where TEntity : class
    {
        private readonly string _uri;
        private HttpClient _httpClient;

        public CommonApiRepository(IServerUrl url)
        {
            _uri = url.GetServerUrl();
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            var contentJObject = await GetRequestAsync(_uri);

            var contentJToken = contentJObject.SelectToken(@"$.listResult");

            var entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(contentJToken.ToString());

            return entities;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var uri = $"{_uri}/{id}";

            var contentJObject = await GetRequestAsync(uri);

            if (contentJObject == null) return null;

            var contentJToken = contentJObject.SelectToken(@"$.itemResult");

            var entity = JsonConvert.DeserializeObject<TEntity>(contentJToken.ToString());

            return entity;
        }


        private async Task<JObject> GetRequestAsync(string uri)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = HttpMethod.Get
            };

            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var contentJObje = JObject.Parse(content);

            return contentJObje;
        }


        public Task<bool> CreateAsync(TEntity entity)
        {
            return CreateOrUpdateAsync(entity, HttpMethod.Put);
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            return CreateOrUpdateAsync(entity, HttpMethod.Put);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{_uri}/{id}"),
                Method = HttpMethod.Delete
            };

            var response = await _httpClient.SendAsync(request);

            return await IsRequestSucceed(response);
        }

        private async Task<bool> CreateOrUpdateAsync(TEntity entity, HttpMethod httpMethod)
        {
            var entityJson = JsonConvert.SerializeObject(entity);

            var content = new StringContent(entityJson);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_uri),
                Method = HttpMethod.Post,
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            return await IsRequestSucceed(response);
        }

        private async Task<bool> IsRequestSucceed(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(content).SelectToken(@"$.resultCode").ToString();

            var resultCode = Convert.ToInt16(result);

            return resultCode == 0;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_httpClient == null) return;
            _httpClient.Dispose();
            _httpClient = null;
        }
    }
}
