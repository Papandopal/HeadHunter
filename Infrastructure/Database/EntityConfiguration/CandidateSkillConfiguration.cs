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
    internal class CandidateSkillConfiguration : IEntityTypeConfiguration<CandidateSkill>
    {
        void IEntityTypeConfiguration<CandidateSkill>.Configure(EntityTypeBuilder<CandidateSkill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();
            builder.HasOne<Skill>().WithMany().HasForeignKey(x=>x.SkillId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
