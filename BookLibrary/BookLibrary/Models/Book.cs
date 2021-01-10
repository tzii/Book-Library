using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.Models
{
    public class Book
    {
        public string _id { set; get; }
        public string name { set; get; }
        public int bought { set; get; }
        public string author { set; get; }
        public string image { set; get; }
        public string description { set; get; }
        public int price { set; get; }
        public List<string> categories { set; get; }
        public List<string> chapters { set; get; }
    }
}
