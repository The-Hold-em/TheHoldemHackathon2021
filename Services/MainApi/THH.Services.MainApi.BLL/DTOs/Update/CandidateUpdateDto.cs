using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.Update;

public class CandidateUpdateDto : IDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Party { get; set; }
    public Guid UpdatedUserId { get; set; }
}
