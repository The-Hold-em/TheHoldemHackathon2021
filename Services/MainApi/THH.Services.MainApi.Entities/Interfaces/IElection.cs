using THH.Core.Interfaces;

namespace THH.Services.MainApi.Entities.Interfaces;

public interface IElection : IEntityBase
{
    string? Name { get; set; }
}
