using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.Update;

public class NodeCreateDto:IDTO
{
    public string Name { get; set; }
    public Guid CityId { get; set; }
    public Guid CreatedUserId { get; set; }
}
