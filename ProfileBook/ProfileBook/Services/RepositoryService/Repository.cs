using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProfileBook.Services.RepositoryService
{
    public class Repository<T> : IRepository<T> where T : BaseModel, new()
    {
        public const string DATABASE_NAME = "user.db";
        private readonly SQLiteConnection database;
        public Repository()
        {
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(
                                            Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
            database.CreateTable<T>();
        }
        public IEnumerable<T> GetAllItems()
        {
            return database.Table<T>().ToList();
        }
        public int DeleteItem(int id)
        {
            return database.Delete<T>(id);
        }
        public T GetItem(int id)
        {
            return database.Get<T>(id);
        }
        public int InsertItem(T item)
        {
            return database.Insert(item);
        }             
        public int UpdateItem(T item)
        {
            return database.Update(item);
        }
    }
}
