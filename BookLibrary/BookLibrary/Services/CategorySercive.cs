using BookLibrary.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public static class CategoryService
    {
        public static Dictionary<string, string> Categories;
        private static void saveDic(List<Category> c)
        {
            Categories = new Dictionary<string, string>();
            foreach (var category in c)
            {
                Categories.Add(category._id, category.name);
            }
        }
        public static async Task<List<Category>> getTop()
        {
            var res = await HTTPService.Get("category/top");
            if (res.StatusCode != HttpStatusCode.OK) return new List<Category>();
            var str = await res.Content.ReadAsStringAsync();
            var categories = (List<Category>)JObject.Parse(str)["payload"].ToObject(typeof(List<Category>));
            return categories;
        }
        public static async Task<List<Category>> getAll()
        {
            var res = await HTTPService.Get("category/all");
            if (res.StatusCode != HttpStatusCode.OK) return new List<Category>();
            var str = await res.Content.ReadAsStringAsync();
            var categories = (List<Category>)JObject.Parse(str)["payload"].ToObject(typeof(List<Category>));
            saveDic(categories);
            return categories;
        }
        public static async Task<List<Book>> getCategory(string id)
        {
            var res = await HTTPService.Get("category/?id="+id);
            if (res.StatusCode != HttpStatusCode.OK) return new List<Book>();
            var str = await res.Content.ReadAsStringAsync();
            var books = (List<Book>)JObject.Parse(str)["payload"].ToObject(typeof(List<Book>));
            return books;
        }
    }
}
