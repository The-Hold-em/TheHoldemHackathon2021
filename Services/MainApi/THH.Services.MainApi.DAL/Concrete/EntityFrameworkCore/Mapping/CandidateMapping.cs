using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;

namespace THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Mapping;
public class CandidateMapping : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.EntityBaseMap();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Party).IsRequired();
    }
}
