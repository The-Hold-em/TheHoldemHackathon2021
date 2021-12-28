using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.Update;

public class PollingStationUpdateDto : IDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CityId { get; set; }
    public Guid UpdatedUserId { get; set; }
}
