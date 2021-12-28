using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;

namespace THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Mapping;

public class CityMapping : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.EntityBaseMap();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Plate).IsRequired();

        builder.HasMany(x=>x.PollingStations)
            .WithOne(x=>x.City)
            .HasForeignKey(x=>x.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Nodes)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Districts)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
