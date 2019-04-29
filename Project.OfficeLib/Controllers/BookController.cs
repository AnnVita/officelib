using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeLib.DB;

namespace Project.OfficeLib.Controllers
{
    [Route("api/OfficeLib/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private OfficeLibContext _context;

        public BookController(OfficeLibContext context)
        {
            _context = context;
        }

        // GET: api/OfficeLib/Book/AllBooks
        [HttpGet("AllBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBookItems()
        {
            var result = _context.Books.OrderBy(p=>p.Title);
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Book/BookList/{offset}
        [HttpGet("BookList/{offset}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItemsList(int offset)
        {
            return await _context.Books.OrderBy(p=>p.Title).Skip(offset).Take(5).ToListAsync();
        }

        // GET: api/OfficeLib/Book/BookList/ByUser/OnHands/{userId}
        [HttpGet("BookList/ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItemsByUser(long userId)
        {
            var userRespCurrent = _context.Responsibilities.Where(p => p.UserId == userId).Select(p => p.BookId);  
            var result = _context.Books.Where(a => userRespCurrent.Contains(a.Id));
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Book/BookList/ByUser/OnHands/{userId}
        [HttpGet("BookList/ByUser/OnHands/{userId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItemsByUserOnHands(long userId)
        {
            var userRespCurrent = _context.Responsibilities.Where(p => p.UserId == userId && p.State == "on_hands").Select(p => p.BookId);  
            var result = _context.Books.Where(a => userRespCurrent.Contains(a.Id));
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Book/BookList/ByUser/History/{userId}
        [HttpGet("BookList/ByUser/History/{userId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItemsByUserHistory(long userId)
        {
            var userRespCurrent = _context.Responsibilities.Where(p => p.UserId == userId && p.State == "returned").Select(p => p.BookId);  
            var result = _context.Books.Where(a => userRespCurrent.Contains(a.Id));
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/BookBookList/SearchByString/{searchString}
        [HttpGet("BookList/SearchByString/{searchString}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookItemsListBySearch(string searchString)
        {
            var result = _context.Books.Where(p => EF.Functions.Like(p.Title, "%" + searchString + "%") || EF.Functions.Like(p.Author, "%" + searchString + "%"));  
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Book/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookItem(long id)
        {
            var item = await _context.Books.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // GET: api/OfficeLib/Book/{id}/CurrentOwner
        [HttpGet("{id}/CurrentOwner")]
        public async Task<ActionResult<User>> GetBookCurrentOwner(long id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            var currBookResponsibles = _context.Responsibilities.Where(p => p.BookId == book.Id && p.State == "on_hands").ToList();
            if (currBookResponsibles.Count > 0)
            {
                var currentOwner = _context.Users.FirstOrDefault(p => p.Id == currBookResponsibles[0].UserId);
                return currentOwner;
            }
            return NotFound("Owner not found");
        }

        // POST: api/OfficeLib/Book
        [HttpPost]
        public async Task<ActionResult<Book>> PostBookItem(Book item)
        {
            _context.Books.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookItem), new { id = item.Id }, item);
        }

        // PUT: api/OfficeLib/Book/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> PutBookItem(long id, [FromBody]Book item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OfficeLib/Book/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookItem(long id)
        {
            var bookItemToDelete = await _context.Books.FindAsync(id);

            if (bookItemToDelete == null)
            {
                return NotFound();
            }

            _context.Books.Remove(bookItemToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}