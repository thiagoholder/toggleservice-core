using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ClientAdministrator
{
    class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] args)
        {
            var client = new HttpClient();
            const string email = "administrator@toggleservice.com", password = "sW5brAwaCu=a";
            var token = await GetTokenAsync(client, email, password);
            Console.WriteLine("Access token: {0}", token);
            Console.WriteLine();
            var resource = await GetResourceAsync(client, token);
            Console.WriteLine("API response: {0}", resource);

            Console.ReadLine();
        }

        public static async Task<string> GetTokenAsync(HttpClient client, string email, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:57425/connect/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "password",
                    ["username"] = email,
                    ["password"] = password
                })
            };

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (payload["error"] != null)
            {
                throw new InvalidOperationException("An error occurred while retrieving an access token.");
            }

            return (string)payload["access_token"];
        }

        public static async Task<string> GetResourceAsync(HttpClient client, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:57425/api/administrator/toggles");
            Console.WriteLine("Acess admin area with all toogles: {0}", request.RequestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            if (response.StatusCode == HttpStatusCode.Forbidden)
                return "You Not Permission to Acess This Area";

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:57425/api/administrator/toggles/ServiceB/isButtonBlue");
            Console.WriteLine("Acess admin area with toggle and Features: {0}", request.RequestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.StatusCode == HttpStatusCode.Forbidden)
                return "You Not Permission to Acess This Area";

            return await response.Content.ReadAsStringAsync();
        }
    }
}
