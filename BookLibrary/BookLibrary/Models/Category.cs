using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.Models
{
    public class Category
    {
        public string _id { set; get; }
        public string name { set; get; }
        public string image { set; get; }
        public string description { set; get; }
        public int count { set; get; }
    }
}
