using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace API.Handlers
{
    public static class VaultHandler
    {
        public static string GetDbDataFromVault()
        {
            var connectionString = string.Empty;
            var vaultToken = string.Empty;
            using (var client = new HttpClient())
            {
                const string jwtPath = "/var/run/secrets/kubernetes.io/serviceaccount/token";
                var jwt = string.Empty;

                try
                {
                    using var sr = new StreamReader(jwtPath);
                    jwt = sr.ReadToEnd();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                var serializedObject = new
                {
                        role = "webapp", jwt
                };

                const string vaultUrl = "http://vault:8200";
                const string authPath = "auth/kubernetes/login";

                client.BaseAddress = new Uri($"{vaultUrl}/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PutAsJsonAsync($"v1/{authPath}", serializedObject).Result;
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        Console.WriteLine("Success 1");
                        dynamic tokenInfo = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        vaultToken = tokenInfo.auth.client_token;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("logged in");
                    }
                }
                else
                {
                    Console.WriteLine("Error 1");
                    Console.WriteLine(response.Content);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }
            }

            using (var client = new HttpClient())
            {
                const string vaultUrl = "http://vault:8200";
                const string secretsPath = "secret/data/webapp/config";

                client.BaseAddress = new Uri($"{vaultUrl}/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-Vault-Token", vaultToken);

                var response = client.GetAsync($"v1/{secretsPath}").Result;

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        Console.WriteLine("Success 2");
                        dynamic secretInfo = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        dynamic ourSecrets = secretInfo.data.data;
                        connectionString = ourSecrets.suppliersconnectionstring;
                        
                        return connectionString;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("connection string here");
                    }
                }
                else
                {
                    Console.WriteLine("Error 2");
                    Console.WriteLine(response.Content);
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.RequestMessage);
                }
            }

            return connectionString;
        }
    }
}