using ProfileBook.Models;
using SQLite;
using System.Collections.Generic;

namespace ProfileBook.SQlite
{
    public class ProfileRepository
    {
        readonly SQLiteConnection database;
        public ProfileRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Profile>();
        }
        public IEnumerable<Profile> GetItems()
        {
            return database.Table<Profile>().ToList();
        }
        public Profile GetItem(int id)
        {
            return database.Get<Profile>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Profile>(id);
        }
        public int SaveItem(Profile item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
