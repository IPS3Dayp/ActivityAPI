using DayPlannerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DayPlannerAPI.Data
{
    public class ActivityDbContext : DbContext
    {
        public ActivityDbContext(DbContextOptions<ActivityDbContext> options) : base(options)
        {
        }

        public DbSet<PlannedActivity> PlannedActivities { get; set; }
    }
}
