using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LmsProject.Models;

namespace LmsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveHistoriesController : ControllerBase
    {
        private readonly LmsDbContext _context;

        public LeaveHistoriesController(LmsDbContext context)
        {
            _context = context;
        }

        // GET: api/LeaveHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveHistory>>> GetLeaveHistories()
        {
            return await _context.LeaveHistories.ToListAsync();
        }

        // GET: api/LeaveHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveHistory>> GetLeaveHistory(int id)
        {
            var leaveHistory = await _context.LeaveHistories.FindAsync(id);

            if (leaveHistory == null)
            {
                return NotFound();
            }

            return leaveHistory;
        }

        // PUT: api/LeaveHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveHistory(int id, LeaveHistory leaveHistory)
        {
            if (id != leaveHistory.LeaveId)
            {
                return BadRequest();
            }

            _context.Entry(leaveHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LeaveHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveHistory>> PostLeaveHistory(LeaveHistory leaveHistory)
        {
            _context.LeaveHistories.Add(leaveHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveHistory", new { id = leaveHistory.LeaveId }, leaveHistory);
        }

        // DELETE: api/LeaveHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveHistory(int id)
        {
            var leaveHistory = await _context.LeaveHistories.FindAsync(id);
            if (leaveHistory == null)
            {
                return NotFound();
            }

            _context.LeaveHistories.Remove(leaveHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveHistoryExists(int id)
        {
            return _context.LeaveHistories.Any(e => e.LeaveId == id);
        }
    }
}
