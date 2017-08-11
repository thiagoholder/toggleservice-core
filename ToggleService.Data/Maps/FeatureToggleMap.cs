using System.Data.Entity.ModelConfiguration;
using ToggleService.Domain;

namespace ToggleService.Data.Maps
{
    public class FeatureToggleMap: EntityTypeConfiguration<FeatureToggle>
    {
        public FeatureToggleMap()
        {
            
            ToTable("FeatureToggle");
            Property(x => x.Enabled)
                .IsRequired()
                .HasColumnType("bit");
            HasKey(x => new {x.IdFeature, x.IdService});
            HasRequired(x => x.Feature)
                .WithMany(x => x.Toggles)
                .HasForeignKey(x => x.IdFeature);
            HasRequired(x => x.Service)
                .WithMany(x => x.Toggles)
                .HasForeignKey(x => x.IdService);
        }
    }
}
