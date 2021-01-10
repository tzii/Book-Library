using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.Models
{
    public class Chapter
    {
        public string _id { set; get; }
        public string name { set; get; }
        public string bookId { set; get; }
        public int number { set; get; }
        public string content { set; get; }
    }
}
