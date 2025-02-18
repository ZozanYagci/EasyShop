using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var token = GetJwtToken();
            return await GetApiResponseAsync<T>(endpoint, token);
        }


        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var token = GetJwtToken();  
            return await PostApiResponseAsync<T>(endpoint, data, token);
        }

        // Jwt token'ı session'dan al
        private string GetJwtToken()
        {
            var context = httpContextAccessor.HttpContext;
            if (context?.Session == null || string.IsNullOrEmpty(context.Session.GetString("JWT")))
            {
                //throw new Exception("HttpContext is null. Lütfen oturum açınız");
                return null;
               
            }

            var token = context.Session.GetString("JWT");

            return token;
        }
        //Get isteği
        private async Task<T> GetApiResponseAsync<T>(string endpoint, string token)
        {
            var client = httpClientFactory.CreateClient();
            var baseUrl = configuration["ApiSettings:BaseUrl"];

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response = await client.GetAsync($"{baseUrl}{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return default;
            }

            throw new Exception($"API çağrısı başarısız oldu: {response.StatusCode}");
        }
        

        // Post isteği
        private async Task<T> PostApiResponseAsync<T>(string endpoint, object data, string token)
        {
            var client = httpClientFactory.CreateClient();
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            }
            var response= await client.PostAsync($"{baseUrl}{endpoint}", content);


            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

                throw new Exception($"API çağrısı başarısız oldu: {response.StatusCode}, Detay: {await response.Content.ReadAsStringAsync()}");
            
        }
    }
}
