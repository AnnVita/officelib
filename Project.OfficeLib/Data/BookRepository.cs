using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeLib.DB;

namespace OfficeLib.Data
{
    public class BookRepository : IRepository<Book>
    {
        private OfficeLibContext _context;

        public BookRepository(OfficeLibContext context)
        {
            _context = context;

            /*
            if (_context.BookItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.BookItems.Add(new BookItem { Title = "BookFromRepository" });
                _context.SaveChanges();
            }
             */
        }

        public IEnumerable<Book> GetAllItems()
        {
            return _context.Books;
        }

        public Book GetItem(long id)
        {
            return _context.Books.Find(id);
        }

        public IEnumerable<Book> GetItemsById(long[] id)
        {
            IEnumerable<Book> result = new Book[] { };
            return result;
        }

        public void Create(Book item)
        {
            _context.Books.Add(item);
        }

        public void Update(Book item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            Book item = _context.Books.Find(id);
            if (item != null)
            {
                _context.Books.Remove(item);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _isDisposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}