using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using OfficeLib.DB;

namespace Project.OfficeLib.Controllers
{
    [Route("api/OfficeLib/Assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private OfficeLibContext _context;

        public AssignmentController(OfficeLibContext context)
        {
            _context = context;
        }

        // GET: api/OfficeLib/Assignment/AssignmentList/ByUser/{userId}
        [HttpGet("AssignmentList/ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentByUser(long userId)
        {
            var result = _context.Assignments.Where(p => p.UserId == userId);
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Assignment/AssignmentList/ByAssigner/{assignerId}
        [HttpGet("AssignmentList/ByAssigner/{assignerId}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentByAssigner(long assignerId)
        {
            var result = _context.Assignments.Where(p => p.AssignerId == assignerId);
            return await result.ToListAsync();
        }

        // GET: api/OfficeLib/Assignment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignmentItem(long id)
        {
            var item = await _context.Assignments.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST: api/OfficeLib/Assignment
        [HttpPost]
        public async Task<ActionResult<Assignment>> PostAssignmentItem(Assignment item)
        {
            item.Date = DateTime.Now;
            _context.Assignments.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssignmentItem), new { id = item.Id }, item);
        }

        // PUT: api/OfficeLib/Assignment/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Assignment>> PutAssignmentItem(long id, [FromBody]Assignment item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OfficeLib/Assignment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignerItem(long id)
        {
            var assignmentItemToDelete = await _context.Assignments.FindAsync(id);

            if (assignmentItemToDelete == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignmentItemToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}