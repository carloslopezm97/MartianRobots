using MartianRobots.Entities.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartianRobots.Entities.Configurations
{
    public class ExecutionConfiguration : IEntityTypeConfiguration<Execution>
    {
        public void Configure(EntityTypeBuilder<Execution> builder)
        {
            builder.ToTable("Execution");

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Robots).WithOne(y => y.Execution);

            builder.HasOne(x => x.Grid).WithOne(y => y.Execution).HasForeignKey<Execution>(y => y.GridId);
        }
    }
}
