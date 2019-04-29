using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeLib.DB;

namespace Project.OfficeLib.Controllers
{
    [Route("api/OfficeLib/Responsibility")]
    [ApiController]
    public class ResponsibilityController : ControllerBase
    {
        private OfficeLibContext _context;

        public ResponsibilityController(OfficeLibContext context)
        {
            _context = context;
        }

        // GET: api/OfficeLib/Responsibility/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Responsibility>> GetResponsibilityItem(long id)
        {
            var item = await _context.Responsibilities.FindAsync(id);
            if (item == null)
            {
                return NotFound("Responsibility item is not found");
            }
            return item;
        }

        // POST: api/OfficeLib/Responsibility
        [HttpPost]
        public async Task<ActionResult<Responsibility>> PostResponsibilitiesItem([FromBody]Responsibility item)
        {
            item.Date = DateTime.Now;
            var currBookResponsibles = _context.Responsibilities.Where(p => p.BookId == item.BookId && p.State == "on_hands").ToList();

            if (currBookResponsibles.Count > 0)
            {
                return BadRequest("This book is already on hands");
            }

            _context.Responsibilities.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResponsibilityItem), new { id = item.Id }, item);
        }

        // PUT: api/OfficeLib/Responsibility/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Responsibility>> PutResponsibilitiesItem(long id, [FromBody]Responsibility item)
        {
            if (id != item.Id)
            {
                return BadRequest("Id mismatch");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}