using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using THH.Services.MainApi.BLL.DTOs.Update;
using THH.Services.MainApi.BLL.DTOs.List;
using THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Contexts;
using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.BLL.Interfaces;
using THH.Shared.Core.Response;
using THH.Shared.Core.ExtensionMethods;
using THH.Services.MainApi.BLL.DTOs.Delete;
using THH.Shared.Core.Services.Interfaces;
using THH.Services.MainApi.BLL.DTOs.Create;

namespace THH.Services.MainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericCommandService<Candidate> _genericCommandService;
        private readonly IGenericQueryService<Candidate> _genericQueryService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMapper _mapper;
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(
            ApplicationDbContext context,
            IGenericCommandService<Candidate> genericCommandService,
            IGenericQueryService<Candidate> genericQueryService,
            ISharedIdentityService sharedIdentityService,
            IMapper mapper,
            ILogger<CandidateController> logger)
        {
            _context = context;
            _genericCommandService = genericCommandService;
            _genericQueryService = genericQueryService;
            _sharedIdentityService = sharedIdentityService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _genericQueryService.GetAllAsync<CandidateListDto>();
            _logger.LogInformation("api/candidate/getall calling enpoint");
            return Response<IEnumerable<CandidateListDto>>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _genericQueryService.GetByIdAsync<CandidateListDto>(id);
            _logger.LogInformation("api/candidate/GetById calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("candidate not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/candidate/GetById",
                    errors: "İstenilen candidate bulunamadı"
                    ).CreateResponseInstance();
            }
            return Response<CandidateListDto>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
      

        [HttpPost]
        public async Task<IActionResult> Create(CandidateCreateDto dto)
        {
            dto.CreatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            var candidate = await _genericCommandService.AddAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/candidate/add calling enpoint");
            var map = _mapper.Map<CandidateListDto>(candidate);
            return Response<CandidateListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpPut]
        public async Task<IActionResult> Update(CandidateUpdateDto dto)
        {
            dto.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            await _genericCommandService.UpdateAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/candidate/update calling enpoint");
            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _genericQueryService.GetByIdAsync<DeleteDto>(id);
            data.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            _logger.LogInformation("api/candidate/delete calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("candidate not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/candidate/delete",
                    errors: "İstenilen candidate bulunamadı"
                    ).CreateResponseInstance();
            }

            await _genericCommandService.RemoveAsync(data);
            await _genericCommandService.SaveChangesAsync();

            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
    }
}
