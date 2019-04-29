using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeLib.DB;

namespace OfficeLib.Data
{
    interface IRepository<T> : IDisposable
            where T : class
    {
        IEnumerable<T> GetAllItems(); 
        T GetItem(long id); 
        IEnumerable<T> GetItemsById(long[] id); 
        void Create(T item); 
        void Update(T item); 
        void Delete(long id); 
        void Save();
    }
}