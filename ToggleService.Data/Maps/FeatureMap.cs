using System.Data.Entity.ModelConfiguration;
using ToggleService.Data.Entities;

namespace ToggleService.Data.Maps
{
    public class FeatureMap : EntityTypeConfiguration<Feature>
    {
        public FeatureMap()
        {
            HasKey(x => x.IdFeature);
            ToTable("Feature");
            Property(x => x.Description).IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");
            Property(p => p.Enabled).IsRequired();
            Property(p => p.Version).IsRequired();
        }
    }
}
