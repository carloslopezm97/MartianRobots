using MartianRobots.Entities.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MartianRobots.Entities.Configurations
{
    public class RobotConfiguration : IEntityTypeConfiguration<Robot>
    {
        public void Configure(EntityTypeBuilder<Robot> builder)
        {
            builder.ToTable("Robot");

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.PositionRecord).WithOne(y => y.Robot);

            builder.HasOne(x => x.Execution).WithMany(y => y.Robots);

            // Delete behaviour to Restrict to prevent cycles between Robot, Grid and Execution
            builder.HasOne(x => x.Grid).WithMany(y => y.Robots).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
