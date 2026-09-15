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
    internal class AccessRuleConfiguration : IEntityTypeConfiguration<AccessRule>
    {
        void IEntityTypeConfiguration<AccessRule>.Configure(EntityTypeBuilder<AccessRule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();
            builder.HasOne(x => x.Skill).WithMany().OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
