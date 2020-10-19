using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.RepositoryService
{
    public interface IRepository<T> where T : BaseModel, new()
    {
        IEnumerable<T> GetAllItems();
        T GetItem(int id);
        int DeleteItem(int id);
        int InsertItem(T item);
        int UpdateItem(T item);
    } 
}
