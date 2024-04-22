using Newtonsoft.Json.Serialization;
using NuGet.Protocol;
using System.Text.Json;
using WebClassLibrary;

namespace CaseStudyMVC.Infrastructure
{
    public static class ApiHelper
    {
        public static async Task<T> ExecuteHttpGet<T>(string url, string token, string baseUrl) where T : class
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;
        }

        public static async Task<TOutput> ExecuteHttpPost<TInput, TOutput>(
            string url,
            string token,
            string baseUrl,
            TInput inputObj
            )
        {
            //JsonSerializer Options object 
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            };

            //Convert the input object to a JSON Content object
            var jsonContent = JsonContent.Create(
                inputValue: inputObj,
                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"),
                options: serializerOptions
            );
            

            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.PostAsync(url,jsonContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TOutput>();
            return result!;
        }

        public static async Task<TOutput> ExecuteHttpPut<TInput, TOutput>(
            string url,
            string token,
            string baseUrl,
            TInput inputObj
            )
        {
            //JsonSerializer Options object 
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            };

            //Convert the input object to a JSON Content object
            var jsonContent = JsonContent.Create(
                inputValue: inputObj,
                mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"),
                options: serializerOptions
            );


            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.PutAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TOutput>();
            return result!;
        }

        public static async Task<TOutput> ExecuteHttpDelete<TInput, TOutput>(
            string url,
            string token,
            string baseUrl
            )
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl!);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TOutput>();
            return result!;
        }
    }
}
