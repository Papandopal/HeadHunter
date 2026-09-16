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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        void IEntityTypeConfiguration<Project>.Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();
            builder.HasMany(x => x.ProjectTags).WithMany(x=>x.Projects);
        }
    }
}
