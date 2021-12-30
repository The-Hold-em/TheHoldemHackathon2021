
using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.Create;

public class CandidateCreateDto : IDTO
{
    public string? Name { get; set; }
    public string? Party { get; set; }
    public Guid CreatedUserId { get; set; }
}
