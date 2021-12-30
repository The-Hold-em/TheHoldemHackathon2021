#nullable disable
using THH.Core.Concrete;
using THH.Services.MainApi.Entities.Interfaces;

namespace THH.Services.MainApi.Entities.Concrete;

public class Candidate:EntityBase,ICandidate
{
    public string Name { get; set; }
    public string Party { get; set; }
}
