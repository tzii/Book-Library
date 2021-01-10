using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.Models
{
    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public int balance { get; set; }
        public List<string> books { set; get; }
    }
}
