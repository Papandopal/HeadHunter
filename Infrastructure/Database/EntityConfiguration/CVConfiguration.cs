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
    internal class CVConfiguration : IEntityTypeConfiguration<CV>
    {
        void IEntityTypeConfiguration<CV>.Configure(EntityTypeBuilder<CV> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();
            builder.HasOne(x => x.Position).WithMany();
            builder.HasOne(x => x.Candidate).WithMany();
        }
    }
}
