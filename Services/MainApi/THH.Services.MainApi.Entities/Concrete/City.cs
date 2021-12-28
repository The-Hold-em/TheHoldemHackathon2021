#nullable disable
using THH.Core.Concrete;
using THH.Services.MainApi.Entities.Interfaces;

namespace THH.Services.MainApi.Entities.Concrete;

public class City:EntityBase,ICity
{
    public string Name { get; set; }
    public int Plate { get; set; }
    public IEnumerable<District> Districts { get; set; }

    public IEnumerable<Node> Nodes { get; set; }
    public IEnumerable<PollingStation> PollingStations { get; set; }

}