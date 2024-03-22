using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TetrisAPI.Models;

namespace TetrisAPI.Data
{
    public class DBContext : IdentityDbContext<IdentityUser>
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
                .HasForeignKey(f => f.UserId);
        }

        public DbSet<Game> GameInfo { get; set; }
    }
}
