using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.List;

public class CityListDto : IDTO
{
    public Guid Id { get; set; }
    public int Name { get; set; }
    public int Plate { get; set; }
    public IEnumerable<DistrictListDto> Districts { get; set; }
}
