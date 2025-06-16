using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;

namespace StockProject.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<UserEntity>(options)
    {
        public DbSet<Point> Points { get; set; }
        public DbSet<StockEntity> Stocks { get; set; }
        public DbSet<StockCategory> StockCategories { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentTime> StudentTimes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StockEntity>().ToTable("Stocks");
            builder.Entity<StockCategory>().ToTable("StockCategories");
            builder.Entity<StockTransaction>().ToTable("StockTransactions");
            builder.Entity<Board>().ToTable("Boards");
            builder.Entity<Point>().ToTable("Points");
            builder.Entity<Student>().ToTable("Students");
            builder.Entity<StudentTime>().ToTable("StudentTimes");


        }
        
    }
}
