using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Utilities.ApiClients
{
    public class ApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public ApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        // Get isteği
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var client = httpClientFactory.CreateClient();
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            var response = await client.GetAsync($"{baseUrl}{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            throw new Exception($"API çağrısı başarısız oldu: {response.StatusCode}");
        }

        // Post isteği
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var client = httpClientFactory.CreateClient();
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response= await client.PostAsync($"{baseUrl}{endpoint}", content);


            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
                throw new Exception($"API çağrısı başarısız oldu: {response.StatusCode}, Detay: {content}");
            
        }
    }
}
