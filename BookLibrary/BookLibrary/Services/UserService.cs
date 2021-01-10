using BookLibrary.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public static class UserService
    {
        private static User user;
        public static string Name
        {
            get
            {
                if (user == null) return "";
                return user.name;
            }
        }
        public static int Balance
        {
            get
            {
                if (user == null) return 0;
                return user.balance;
            }
        }
        public static string Email
        {
            get
            {
                if (user == null) return "";
                return user.email;
            }
        }
        public static List<string> Books
        {
            get
            {
                if (user == null) return new List<string>();
                return user.books;
            }
        }
        public async static Task<Tuple<bool, string>> Login(string email, string password)
        {
            var obj = new JObject
            {
                { "email", email },
                { "password", password }
            };
            var res = await HTTPService.Post("user/login", obj);
            var str = await res.Content.ReadAsStringAsync();
            var status = JObject.Parse(str)["status"].ToString();
            if (status == "ok")
            {
                HTTPService.AddAthencationToken(JObject.Parse(str).SelectToken("payload.authorization").ToString());
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                var msg = JObject.Parse(str)["msg"].ToString();
                return new Tuple<bool, string>(false, msg);
            }
        }
        public async static Task<bool> Logout()
        {
            var res = await HTTPService.Get("user/logout");
            return res.StatusCode == HttpStatusCode.OK;
        }
        public async static Task<Tuple<bool, string>> Signup(string name, string email, string password)
        {
            var obj = new JObject
            {
                { "name", name },
                { "email", email },
                { "password", password }
            };
            var res = await HTTPService.Post("user/register", obj);
            var str = await res.Content.ReadAsStringAsync();
            var status = JObject.Parse(str)["status"].ToString();
            var msg = JObject.Parse(str)["msg"].ToString();
            if (status == "err") return new Tuple<bool, string>(false, msg);
            return new Tuple<bool, string>(true, msg);
        }
        public async static Task<bool> LoadInfo()
        {
            //if (user != null) return true;
            var res = await HTTPService.Get("user");
            if (res.StatusCode != HttpStatusCode.OK) return false;
            var str = await res.Content.ReadAsStringAsync();
            user = (User)JObject.Parse(str)["payload"].ToObject(typeof(User));
            return true;
        }
        public async static Task<Tuple<bool,string>> BuyBook(string id)
        {
            var obj = new JObject
            {
                { "bookId", id }
            };
            var res = await HTTPService.Post("user/buy", obj);
            var str = await res.Content.ReadAsStringAsync();
            var status = JObject.Parse(str)["status"].ToString();
            var msg = JObject.Parse(str)["msg"].ToString();
            if (status == "err") return new Tuple<bool, string>(false, msg);
            LoadInfo();
            return new Tuple<bool, string>(true, msg);
        }
    }
}
