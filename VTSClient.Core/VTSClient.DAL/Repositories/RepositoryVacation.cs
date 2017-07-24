using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VTSClient.DAL.Entities;
using VTSClient.DAL.Interfaces;

namespace VTSClient.DAL.Repositories
{
    public class RepositoryVacation : IRepositoryVacation
    {
        private readonly string _uri;
        private readonly HttpClient _httpClient;

        public RepositoryVacation()
        {
            _uri = "http://localhost:5000/vts/workflow";
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Vacation>> GetAsync()
        {
            var contentJObject = await GetRequestAsync(_uri);

            var contentJToken = contentJObject.SelectToken(@"$.listResult");

            var entities = JsonConvert.DeserializeObject<IEnumerable<Vacation>>(contentJToken.ToString());

            return entities;
        }

        public async Task<Vacation> GetByIdAsync(Guid id)
        {
            var uri = $"{_uri}/{id}";

            var contentJObject = await GetRequestAsync(uri);

            if (contentJObject == null) return null;
            var contentJToken = contentJObject.SelectToken(@"$.itemResult");

            var entity = JsonConvert.DeserializeObject<Vacation>(contentJToken.ToString());

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

            request.Headers.Add("token", "d97b97e0-b73d-4534-8cf2-3ae80bd3f1e6");

            var response1 = _httpClient.SendAsync(request);

            var res = response1.Result;

            var content = await res.Content.ReadAsStringAsync();
            var contentJObje = JObject.Parse(content);

            return contentJObje;
        }


        public Task<bool> CreateAsync(Vacation entity)
        {
            return CreateOrUpdateAsync(entity, HttpMethod.Put);
        }

        public Task<bool> UpdateAsync(Vacation entity)
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

            HttpResponseMessage response = _httpClient.SendAsync(request).Result;

            return await IsRequestSucceed(response);
        }

        private async Task<bool> CreateOrUpdateAsync(Vacation entity, HttpMethod httpMethod)
        {
            string entityJson = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(entityJson);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_uri),
                Method = HttpMethod.Post,
                Content = content
            };

            HttpResponseMessage response = _httpClient.SendAsync(request).Result;

            return await IsRequestSucceed(response);
        }

        private async Task<bool> IsRequestSucceed(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(content).SelectToken(@"$.resultCode").ToString();
            var resultCode = Convert.ToInt16(result);

            return resultCode == 0;
        }
    }
}
