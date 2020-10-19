using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    [Table("Users")]
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
