#nullable disable
using THH.Core.Concrete;
using THH.Services.MainApi.Entities.Interfaces;

namespace THH.Services.MainApi.Entities.Concrete;

public class District:EntityBase, IDistrict
{
    public string Name { get; set; }

    public City City { get; set; }
    public Guid CityId { get; set; }
}
