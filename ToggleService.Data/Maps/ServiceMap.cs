using System.Data.Entity.ModelConfiguration;
using ToggleService.Data.Entities;

namespace ToggleService.Data.Maps
{
    public class ServiceMap: EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            HasKey(x => x.Id);
            ToTable("Service");
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");
            HasMany(x => x.FeaturesToggles)
                .WithRequired(x => x.Service)
                .HasForeignKey(x => x.IdService);
        }

    }
}
