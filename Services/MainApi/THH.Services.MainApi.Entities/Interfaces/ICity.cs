using THH.Core.Interfaces;
using THH.Services.MainApi.Entities.Concrete;

namespace THH.Services.MainApi.Entities.Interfaces;

public interface ICity:IEntityBase
{
    string Name { get; set; }
    int Plate { get; set; }
    IEnumerable<District> Districts { get; set; }
}
