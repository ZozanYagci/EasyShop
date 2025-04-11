using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.ApiClients
{
    public class ApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        //Header'a Authorization: Bearer <token>
        //API, bu token'ı kontrol edip sadece doğrulanmış kullanıcıya veri döndürecek.
        private void AddAuthorizationHeader(HttpClient client)
        {
            var accessToken = httpContextAccessor.HttpContext?.Request?.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        //Get isteği
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var client = httpClientFactory.CreateClient();

            AddAuthorizationHeader(client);

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

            AddAuthorizationHeader(client);

            var baseUrl = configuration["ApiSettings:BaseUrl"];
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{baseUrl}{endpoint}", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            throw new Exception($"API çağrısı başarısız oldu: {response.StatusCode}, Detay: {await response.Content.ReadAsStringAsync()}");

        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var client = httpClientFactory.CreateClient();

            AddAuthorizationHeader(client);

            var baseUrl = configuration["ApiSettings:BaseUrl"];
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{baseUrl}{endpoint}", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            throw new Exception($"API çağrısı başarısız oldu: {response.StatusCode}, Detay: {await response.Content.ReadAsStringAsync()}");
        }

    }
}
