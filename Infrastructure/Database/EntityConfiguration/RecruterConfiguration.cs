using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfiguration
{
    public class RecruterConfiguration : IEntityTypeConfiguration<Recruter>
    {
        void IEntityTypeConfiguration<Recruter>.Configure(EntityTypeBuilder<Recruter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                  .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.SequentialGuidValueGenerator>();
            builder.HasMany(x => x.LikedCVs).WithMany(x=>x.LikedRecruters).UsingEntity<Dictionary<string, object>>(
                   "CVRecruter",
                   j => j.HasOne<CV>().WithMany().HasForeignKey("LikedCVsId").OnDelete(DeleteBehavior.NoAction),
                   j => j.HasOne<Recruter>().WithMany().HasForeignKey("LikedRecrutersId").OnDelete(DeleteBehavior.NoAction)
               );
        }   
    }
}
