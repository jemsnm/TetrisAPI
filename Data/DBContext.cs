using Microsoft.EntityFrameworkCore;
using TetrisAPI.Models;

namespace TetrisAPI.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasOne(t => t.User)
                .WithMany(t => t.Games)
                .HasForeignKey(fk => fk.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> GameInfo { get; set; }
    }
}
