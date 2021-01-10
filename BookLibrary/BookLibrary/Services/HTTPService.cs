using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BookLibrary.Services
{
    public static class HTTPService
    {
        private static HttpClient client;
        private static string URL;
        private static string JsonType;
        private static string token;
        private static string Token
        {
            set
            {
                token = value;
                Preferences.Set("token", value);
            }
            get
            {
                if (token == null) return Preferences.Get("token", "");
                return token;
            }
        }
        public static void Settings()
        {
            client = new HttpClient();
            URL = "https://booklibrary-ie307.herokuapp.com/api/";
            JsonType = "application/json";
            if (Token != "") AddAthencationToken(Token);
        }
        public static void RemoveAuthencationToken()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            Token = "";
        }
        public static void AddAthencationToken(string _token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Token = _token;
        }
        public static async Task<string> Login(string email, string password)
        {
            var obj = new JObject();
            obj.Add("email", email);
            obj.Add("password", password);
            var content = new StringContent(obj.ToString(), Encoding.UTF8, JsonType);
            var result = await client.PostAsync(URL + "user/login", content);
            return await result.Content.ReadAsStringAsync();
        }
        public static async Task<HttpResponseMessage> Post(string link, JObject data)
        {
            var content = new StringContent(data.ToString(), Encoding.UTF8, JsonType);
            return await client.PostAsync(URL + link, content);
        }
        public static async Task<HttpResponseMessage> Get(string link)
        {
            return await client.GetAsync(URL + link);
        }
    }
}
