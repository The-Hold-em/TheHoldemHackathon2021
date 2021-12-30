
using THH.Core.Interfaces;

namespace THH.Services.MainApi.BLL.DTOs.List;

public class CandidateListDto : IDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Party { get; set; }
}
