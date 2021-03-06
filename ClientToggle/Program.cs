﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ClientToggle
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] args)
        {
            var client = new HttpClient();
            const string email = "clientc@toggleservice.com", password = "c=ba?UdRet5C";
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
            Console.WriteLine("Acess admin area: {0}", request.RequestUri);
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                Console.WriteLine("You not Allowed to Acess This Area");


            request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:57425/api/features");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine("Accessing the features available to this client: {0}", request.RequestUri);
            response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            if(response.StatusCode == HttpStatusCode.Unauthorized)
                return "You Not Allowed to Acess This Area";

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:57425/api/features/isButtonBlue");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine("Accessing the features available to this client through the name: isButtonBlue - {0}", request.RequestUri);
            response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return "You Not Allowed to Acess This Area";

            return await response.Content.ReadAsStringAsync();
        }
    }
}
