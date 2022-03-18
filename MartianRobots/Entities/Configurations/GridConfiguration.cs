using MartianRobots.Entities.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartianRobots.Entities.Configurations
{
    public class GridConfiguration : IEntityTypeConfiguration<Grid>
    {
        public void Configure(EntityTypeBuilder<Grid> builder)
        {
            builder.ToTable("Grid");

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Robots).WithOne(y => y.Grid);
        }
    }
}
