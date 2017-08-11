using System.Data.Entity.ModelConfiguration;
using ToggleService.Domain;

namespace ToggleService.Data.Maps
{
    public class FeatureToggleMap: EntityTypeConfiguration<FeatureToggle>
    {
        public FeatureToggleMap()
        {
            HasKey(x => x.Id);
            ToTable("FeatureToggle");
            Property(x => x.Enabled)
                .IsRequired()
                .HasColumnType("bit");
            HasRequired(x => x.Feature)
                .WithMany()
                .HasForeignKey(x => x.IdFeature);
            HasRequired(x => x.Service)
                .WithMany(x => x.FeaturesToggles)
                .HasForeignKey(x => x.IdService)
                .WillCascadeOnDelete();
        }
    }
}
