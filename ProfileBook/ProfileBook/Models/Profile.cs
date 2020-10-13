using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string ProfileImage { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}
