using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject
{
    public static class ApiHelper
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseUrl = "http://localhost:3300/api";
        public static async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            client.DefaultRequestHeaders.Clear();

            if (AuthManager.IsLoggedIn())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthManager.Token);
            }

            return await client.GetAsync($"{BaseUrl}/{endpoint}");
        }

        public static async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
        {
            client.DefaultRequestHeaders.Clear();

            if (AuthManager.IsLoggedIn())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthManager.Token);
            }

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync($"{BaseUrl}/{endpoint}", content);
        }

        public static async Task<HttpResponseMessage> PutAsync(string endpoint, object? data)
        {
            client.DefaultRequestHeaders.Clear();

            if (AuthManager.IsLoggedIn())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthManager.Token);
            }

            if (data == null)
            {
                var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
                return await client.PutAsync($"{BaseUrl}/{endpoint}", emptyContent);
            }

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PutAsync($"{BaseUrl}/{endpoint}", content);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            client.DefaultRequestHeaders.Clear();

            if (AuthManager.IsLoggedIn())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AuthManager.Token);
            }

            return await client.DeleteAsync($"{BaseUrl}/{endpoint}");
        }

        public static bool IsUnauthorized(HttpResponseMessage response)
        {
            return response.StatusCode == System.Net.HttpStatusCode.Unauthorized;
        }

        public static bool IsForbidden(HttpResponseMessage response)
        {
            return response.StatusCode == System.Net.HttpStatusCode.Forbidden;
        }
    }
}


