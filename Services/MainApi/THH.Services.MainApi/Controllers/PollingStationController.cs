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
    public class PollingStationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericCommandService<PollingStation> _genericCommandService;
        private readonly IGenericQueryService<PollingStation> _genericQueryService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMapper _mapper;
        private readonly ILogger<PollingStationController> _logger;

        public PollingStationController(
            ApplicationDbContext context,
            IGenericCommandService<PollingStation> genericCommandService,
            IGenericQueryService<PollingStation> genericQueryService,
            ISharedIdentityService sharedIdentityService,
            IMapper mapper,
            ILogger<PollingStationController> logger)
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
            var data = await _genericQueryService.GetAllAsync<PollingStationListDto>();
            _logger.LogInformation("api/pollingStation/getall calling enpoint");
            return Response<IEnumerable<PollingStationListDto>>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _genericQueryService.GetByIdAsync<PollingStationListDto>(id);
            _logger.LogInformation("api/pollingStation/GetById calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("pollingStation not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/pollingStation/GetById",
                    errors: "İstenilen pollingStation bulunamadı"
                    ).CreateResponseInstance();
            }
            return Response<PollingStationListDto>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
        //todo custom repository içerisine dahil edilecek
        [HttpGet("city/{cityId:guid}")]
        public async Task<IActionResult> GetByCityId(Guid cityId)
        {
            var data = await _context.PollingStations.Where(x => x.CityId == cityId).FirstOrDefaultAsync();
            _logger.LogInformation("api/pollingStation/GetByCityId calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("pollingStation not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/pollingStation/GetByCityId",
                    errors: "İstenilen pollingStation bulunamadı"
                    ).CreateResponseInstance();
            }
            var map = _mapper.Map<PollingStationListDto>(data);
            return Response<PollingStationListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }

        //todo custom repository içerisine dahil edilecek
        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var data = await _context.PollingStations.Where(x => x.Name == name).FirstOrDefaultAsync();
            _logger.LogInformation("api/pollingStation/GetByName calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("pollingStation not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/pollingStation/GetByName",
                    errors: "İstenilen pollingStation bulunamadı"
                    ).CreateResponseInstance();
            }
            var map = _mapper.Map<PollingStationListDto>(data);
            return Response<PollingStationListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PollingStationCreateDto dto)
        {
            dto.CreatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            var pollingStation = await _genericCommandService.AddAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/pollingStation/add calling enpoint");
            var map = _mapper.Map<PollingStationListDto>(pollingStation);
            return Response<PollingStationListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpPut]
        public async Task<IActionResult> Update(PollingStationUpdateDto dto)
        {
            dto.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            await _genericCommandService.UpdateAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/pollingStation/update calling enpoint");
            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _genericQueryService.GetByIdAsync<DeleteDto>(id);
            data.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            _logger.LogInformation("api/pollingStation/delete calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("pollingStation not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/pollingStation/delete",
                    errors: "İstenilen pollingStation bulunamadı"
                    ).CreateResponseInstance();
            }

            await _genericCommandService.RemoveAsync(data);
            await _genericCommandService.SaveChangesAsync();

            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
    }
}
