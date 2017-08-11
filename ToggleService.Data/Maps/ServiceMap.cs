using System.Data.Entity.ModelConfiguration;
using ToggleService.Domain;

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
        }

    }
}
