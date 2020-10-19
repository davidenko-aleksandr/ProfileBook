using SQLite;

namespace ProfileBook.Models
{
    public abstract class BaseModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
    }
}
