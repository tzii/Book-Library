using BookLibrary.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public static class BookService
    {
        public static async Task<List<Book>> getRecommended()
        {
            var res = await HTTPService.Get("book/top");
            if (res.StatusCode != HttpStatusCode.OK) return new List<Book>();
            var str = await res.Content.ReadAsStringAsync();
            var books = (List<Book>)JObject.Parse(str)["payload"].ToObject(typeof(List<Book>));
            return books;
        }
        public static async Task<List<Book>> getNewBooks()
        {
            var res = await HTTPService.Get("book/new");
            if (res.StatusCode != HttpStatusCode.OK) return new List<Book>();
            var str = await res.Content.ReadAsStringAsync();
            var books = (List<Book>)JObject.Parse(str)["payload"].ToObject(typeof(List<Book>));
            return books;
        }
        public static async Task<List<Book>> getAll()
        {
            var res = await HTTPService.Get("book/all");
            if (res.StatusCode != HttpStatusCode.OK) return new List<Book>();
            var str = await res.Content.ReadAsStringAsync();
            var books = (List<Book>)JObject.Parse(str)["payload"].ToObject(typeof(List<Book>));
            return books;
        }
        public static async Task<Book> getBookById(string id)
        {
            var res = await HTTPService.Get("book/"+id);
            if (res.StatusCode != HttpStatusCode.OK) return new Book();
            var str = await res.Content.ReadAsStringAsync();
            var book = (Book)JObject.Parse(str)["payload"].ToObject(typeof(Book));
            return book;
        }
        public static async Task<List<Chapter>> getAllChaptersByBookId(string id)
        {
            var res = await HTTPService.Get("book/chapters/" + id);
            if (res.StatusCode != HttpStatusCode.OK) return new List<Chapter>();
            var str = await res.Content.ReadAsStringAsync();
            var chapters = (List<Chapter>)JObject.Parse(str)["payload"].ToObject(typeof(List<Chapter>));
            return chapters;
        }
        public static async Task<List<Book>> getLastRead()
        {
            var res = await HTTPService.Get("book/lastread");
            if (res.StatusCode != HttpStatusCode.OK) return new List<Book>();
            var str = await res.Content.ReadAsStringAsync();
            var books = (List<Book>)JObject.Parse(str)["payload"].ToObject(typeof(List<Book>));
            return books;
        }
        public static async Task<List<Book>> getOwnBooks()
        {
            var res = await HTTPService.Get("book/own");
            if (res.StatusCode != HttpStatusCode.OK) return new List<Book>();
            var str = await res.Content.ReadAsStringAsync();
            var books = (List<Book>)JObject.Parse(str)["payload"].ToObject(typeof(List<Book>));
            return books;
        }
    }
}
