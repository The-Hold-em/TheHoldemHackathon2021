using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.Update;

public class ElectionUpdateDto : IDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid UpdatedUserId { get; set; }
}
