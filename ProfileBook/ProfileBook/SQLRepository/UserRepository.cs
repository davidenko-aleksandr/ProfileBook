using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileBook.SQLRepository
{
    public class UserRepository
    {
        readonly SQLiteConnection dataBase;
        public UserRepository(string databasePath)
        {
            dataBase = new SQLiteConnection(databasePath);
            dataBase.CreateTable<User>();
        }
        public int SaveItem(User item)
        {
            if (item.Id != 0)
            {
                dataBase.Update(item);
                return item.Id;
            }
            else
            {
                return dataBase.Insert(item);
            }
        }
        public User GetItem(int id)
        {
            return dataBase.Get<User>(id);
        }
        public IEnumerable<User> GetItems()
        {
            return dataBase.Table<User>().ToList();
        }


        //Проверка наличия логина в Б.Д. Это сомнительное решение позже придумаю вариант по-лучше
        public bool ChekLogin(string login)
        {
            IEnumerable<User> collectionUsers = GetItems();
            var loginDB = from log in collectionUsers select log.Login;
            string result = "result";
            foreach (var item in loginDB)
            {
                if (login.Equals(item))
                    result = item;                
            }
            if (result == "result")
                return false;
            else return true;            
        }
    }
}
