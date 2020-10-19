using SQLite;
using System;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class Profile : BaseModel
    {
        public int User_Id { get; set; }
        public string ProfileImage { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTimePr { get; set; }
    }
}
