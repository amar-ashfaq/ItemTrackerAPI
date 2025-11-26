using Microsoft.EntityFrameworkCore;

namespace ItemTrackerAPI
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions options) : base(options){ }

        public DbSet<ItemEntity> Items { get; set; }
    }
}
