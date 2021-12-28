using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.List;

public class PollingStationListDto : IDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CityId { get; set; }
}
