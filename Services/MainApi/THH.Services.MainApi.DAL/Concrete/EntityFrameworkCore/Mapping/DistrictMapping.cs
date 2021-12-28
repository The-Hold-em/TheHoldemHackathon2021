using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;

namespace THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Mapping;

public class DistrictMapping : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.EntityBaseMap();
        builder.Property(x => x.Name).IsRequired();
    }
}
