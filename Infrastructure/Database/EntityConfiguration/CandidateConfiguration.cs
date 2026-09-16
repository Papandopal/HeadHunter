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
    internal class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        void IEntityTypeConfiguration<Candidate>.Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();
            builder.HasMany(x => x.Skills).WithOne().HasForeignKey(x=>x.CandidateId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Projects).WithOne();
        }
    }
}
