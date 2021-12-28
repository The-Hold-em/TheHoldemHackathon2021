using THH.Core.Concrete;
using THH.Services.MainApi.Entities.Interfaces;

namespace THH.Services.MainApi.Entities.Concrete;

public class Election : EntityBase, IElection
{
    public string? Name { get; set; }
}
