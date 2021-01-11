using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookLibrary.Models;
using Newtonsoft.Json.Linq;

namespace BookLibrary.Services
{
    public static class ChapterService
    {
        public static async Task<Chapter> getChapterbyId(string id)
        {
            var res = await HTTPService.Get("book/chapter/"+id);
            if (res.StatusCode != HttpStatusCode.OK) return new Chapter();
            var str = await res.Content.ReadAsStringAsync();
            var status = JObject.Parse(str)["status"].ToString();
            if (status == "err") return new Chapter();
            var chapter = (Chapter)JObject.Parse(str)["payload"].ToObject(typeof(Chapter));
            return chapter;
        }
    }
}
