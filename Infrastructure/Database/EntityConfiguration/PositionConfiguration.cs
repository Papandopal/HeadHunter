using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfiguration
{
    internal class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        void IEntityTypeConfiguration<Position>.Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();
            builder.HasMany(x => x.AccessRules).WithOne(x => x.Position).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.PositionSkills).WithOne(x => x.Position).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.ProjectTags).WithMany(x => x.Positions);
        }
    }
}
