using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.SQlite
{
    public class SQLiteRepository<T> : ISQLiteRepository<T>
    {
        //readonly SQLiteConnection database;
        //public SQLiteRepository(string databasePath)
        //{
        //    database = new SQLiteConnection(databasePath);
        //    database.CreateTable<T>();
        //}
        //public int DeleteItem(int id)
        //{
        //    return database.Delete<T>(id);
        //}

        //public void GetItem(int id)
        //{
        //    database.Get<T>(id);
        //}

        //public IEnumerable<T> GetItems()
        //{
        //    return database.Table<T>().ToList();
        //}

        //public int SaveItem(T item)
        //{
        //    if (item.Id != 0)
        //    {
        //        database.Update(item);
        //        return item.Id;
        //    }
        //    else
        //    {
        //        return database.Insert(item);
        //    }
        //}
    }
}
