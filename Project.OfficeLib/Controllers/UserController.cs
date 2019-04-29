using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeLib.DB;

namespace Project.OfficeLib.Controllers
{
    [Route("api/OfficeLib/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private OfficeLibContext _context;

        public UserController(OfficeLibContext context)
        {
            _context = context;
        }

        // GET: api/OfficeLib/User/AllUsers
        [HttpGet("/AllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUserItems()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/OfficeLib/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserItem(long id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null)
            {
                return NotFound("User not found");
            }
            return item;
        }

        // GET: api/OfficeLib/User/UserList/SearchByString/{searchString}
        [HttpGet("UserList/SearchByString/{searchString}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserItemsListBySearch(string searchString)
        {
            var result = _context.Users.Where(p => EF.Functions.Like(p.Name, "%" + searchString + "%") || EF.Functions.Like(p.Email, "%" + searchString + "%"));  
            return await result.ToListAsync();
        }

        // POST: api/OfficeLib/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUserItem(User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserItem), new { id = item.Id }, item);
        }

        // PUT: api/OfficeLib/User/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUserItem(long id, [FromBody]User item)
        {
            if (id != item.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OfficeLib/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserItem(long id)
        {
            var userItemToDelete = await _context.Users.FindAsync(id);
            if (userItemToDelete == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(userItemToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}