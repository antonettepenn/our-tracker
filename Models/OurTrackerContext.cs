using Microsoft.EntityFrameworkCore;

namespace OurTracker.Models;

public class OurTrackerContext : DbContext
{
    public OurTrackerContext(DbContextOptions<OurTrackerContext> options) : base(options) { }

    public DbSet<Entry> Entries { get; set; }
}
