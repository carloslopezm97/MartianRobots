using MartianRobots.Entities.Configurations;
using MartianRobots.Entities.SQL;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GridConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new RobotConfiguration());
            modelBuilder.ApplyConfiguration(new ExecutionConfiguration());
        }

        public DbSet<Grid> Grids { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<Execution> Executions { get; set; }
    }
}
