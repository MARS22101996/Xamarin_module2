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
    public class ApiRepositoryVacation : IApiRepositoryVacation
    {
        private readonly string _uri;
        private readonly HttpClient _httpClient;

        public ApiRepositoryVacation()
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

            var response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

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

            var response = await  _httpClient.SendAsync(request);

            return await IsRequestSucceed(response);
        }

        private async Task<bool> CreateOrUpdateAsync(Vacation entity, HttpMethod httpMethod)
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
    }
}
