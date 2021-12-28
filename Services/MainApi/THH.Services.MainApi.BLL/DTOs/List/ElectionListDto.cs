using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.List;

public class ElectionListDto : IDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
