using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.Delete;

public class DeleteDto:IDTO
{
    public Guid Id { get; set; }
    public Guid UpdatedUserId { get; set; }
}
