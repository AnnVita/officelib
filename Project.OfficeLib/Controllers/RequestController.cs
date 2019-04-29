using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using OfficeLib.DB;

namespace Project.OfficeLib.Controllers
{
    [Route("api/OfficeLib/Request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private OfficeLibContext _context;

        public RequestController(OfficeLibContext context)
        {
            _context = context;
        }

        // GET: api/OfficeLib/Request/RequestList/ByUser/{userId}
        [HttpGet("RequestList/ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestByUser(long userId)
        {
            var result = _context.Requests.Where(p => p.UserId == userId);
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Request/AllRequest
        [HttpGet("AllRequest")]
        public async Task<ActionResult<IEnumerable<Request>>> GetAllRequestItems()
        {
            var result = _context.Requests.OrderBy(p=>p.Title);
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Request/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequestItem(long id)
        {
            var item = await _context.Requests.FindAsync(id);
            if (item == null)
            {
                return NotFound("Request not found");
            }
            return item;
        }

        // POST: api/OfficeLib/Request
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequestItem([FromBody]Request item)
        {
            item.Date = DateTime.Now;
            _context.Requests.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequestItem), new { id = item.Id }, item);
        }

        // PUT: api/OfficeLib/Request/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Request>> PutRequestItem(long id, [FromBody]Request item)
        {
            if (id != item.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OfficeLib/Request/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestItem(long id)
        {
            var requestItemToDelete = await _context.Requests.FindAsync(id);

            if (requestItemToDelete == null)
            {
                return NotFound("Request not found");
            }

            _context.Requests.Remove(requestItemToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}