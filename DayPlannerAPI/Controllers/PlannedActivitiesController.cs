using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DayPlannerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using DayPlannerAPI.Data;

namespace DayPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannedActivitiesController : ControllerBase
    {
        private readonly ActivityDbContext _context;

        public PlannedActivitiesController(ActivityDbContext context)
        {
            _context = context;
        }

        // GET: api/PlannedActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlannedActivity>>> GetPlannedActivities()
        {
            return await _context.PlannedActivities.ToListAsync();
        }

        [HttpGet("currentdatetime")]
        public async Task<ActionResult<IEnumerable<PlannedActivity>>> GetPlannedActivitiesByCurrentDateTime()
        {
            // Get the current date without the time part
            DateTime currentDate = DateTime.Today;

            // Get all planned activities for the current day
            var plannedActivities = await _context.PlannedActivities
                .Where(activity => activity.StartTime.Date == currentDate)
                .ToListAsync();

            return plannedActivities;
        }

        // GET: api/PlannedActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlannedActivity>> GetPlannedActivity(int id)
        {
            var plannedActivity = await _context.PlannedActivities.FindAsync(id);

            if (plannedActivity == null)
            {
                return NotFound();
            }

            return plannedActivity;
        }

        // POST: api/PlannedActivities
        [HttpPost]
        public async Task<ActionResult<PlannedActivity>> PostPlannedActivity(PlannedActivity plannedActivity)
        {
            _context.PlannedActivities.Add(plannedActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlannedActivity), new { id = plannedActivity.Id }, plannedActivity);
        }

        // PUT: api/PlannedActivities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlannedActivity(int id, PlannedActivity plannedActivity)
        {
            if (id != plannedActivity.Id)
            {
                return BadRequest();
            }

            _context.Entry(plannedActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlannedActivityExists(id))
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

        // DELETE: api/PlannedActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlannedActivity(int id)
        {
            var plannedActivity = await _context.PlannedActivities.FindAsync(id);
            if (plannedActivity == null)
            {
                return NotFound();
            }

            _context.PlannedActivities.Remove(plannedActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlannedActivityExists(int id)
        {
            return _context.PlannedActivities.Any(e => e.Id == id);
        }
    }
}

