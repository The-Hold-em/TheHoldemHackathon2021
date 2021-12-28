using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using THH.Services.MainApi.BLL.DTOs.Update;
using THH.Services.MainApi.BLL.DTOs.List;
using THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Contexts;
using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.BLL.Interfaces;
using THH.Shared.Core.Response;
using THH.Shared.Core.ExtensionMethods;
using THH.Services.MainApi.BLL.DTOs.Delete;
using THH.Shared.Core.Services.Interfaces;

namespace THH.Services.MainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericCommandService<Election> _genericCommandService;
        private readonly IGenericQueryService<Election> _genericQueryService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMapper _mapper;
        private readonly ILogger<ElectionController> _logger;

        public ElectionController(
            ApplicationDbContext context,
            IGenericCommandService<Election> genericCommandService,
            IGenericQueryService<Election> genericQueryService,
            ISharedIdentityService sharedIdentityService,
            IMapper mapper,
            ILogger<ElectionController> logger)
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
            var data = await _genericQueryService.GetAllAsync<ElectionListDto>();
            _logger.LogInformation("api/election/getall calling enpoint");
            return Response<IEnumerable<ElectionListDto>>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _genericQueryService.GetByIdAsync<ElectionListDto>(id);
            _logger.LogInformation("api/election/GetById calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("election not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/election/GetById",
                    errors: "İstenilen election bulunamadı"
                    ).CreateResponseInstance();
            }
            return Response<ElectionListDto>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }

        //todo custom repository içerisine dahil edilecek
        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var data = await _context.Elections.Where(x => x.Name == name).FirstOrDefaultAsync();
            _logger.LogInformation("api/election/GetByName calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("election not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/election/GetByName",
                    errors: "İstenilen election bulunamadı"
                    ).CreateResponseInstance();
            }
            var map = _mapper.Map<ElectionListDto>(data);
            return Response<ElectionListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ElectionCreateDto dto)
        {
            dto.CreatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            var election = await _genericCommandService.AddAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/election/add calling enpoint");
            var map = _mapper.Map<ElectionListDto>(election);
            return Response<ElectionListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpPut]
        public async Task<IActionResult> Update(ElectionUpdateDto dto)
        {
            dto.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            await _genericCommandService.UpdateAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/election/update calling enpoint");
            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _genericQueryService.GetByIdAsync<DeleteDto>(id);
            data.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            _logger.LogInformation("api/election/delete calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("election not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/election/delete",
                    errors: "İstenilen election bulunamadı"
                    ).CreateResponseInstance();
            }

            await _genericCommandService.RemoveAsync(data);
            await _genericCommandService.SaveChangesAsync();

            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
    }
}
