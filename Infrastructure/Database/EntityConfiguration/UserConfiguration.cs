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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne<Candidate>().WithOne().HasForeignKey<Candidate>(x => x.UserId).HasPrincipalKey<User>(x=>x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Recruter>().WithOne().HasForeignKey<Recruter>(x=>x.OwnerId).HasPrincipalKey<User>(x=>x.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
