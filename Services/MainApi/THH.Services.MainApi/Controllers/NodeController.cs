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
using Microsoft.AspNetCore.Authorization;

namespace THH.Services.MainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericCommandService<Node> _genericCommandService;
        private readonly IGenericQueryService<Node> _genericQueryService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMapper _mapper;
        private readonly ILogger<NodeController> _logger;

        public NodeController(
            ApplicationDbContext context,
            IGenericCommandService<Node> genericCommandService,
            IGenericQueryService<Node> genericQueryService,
            ISharedIdentityService sharedIdentityService,
            IMapper mapper,
            ILogger<NodeController> logger)
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
            var data = await _genericQueryService.GetAllAsync<NodeListDto>();
            _logger.LogInformation("api/node/getall calling enpoint");
            return Response<IEnumerable<NodeListDto>>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) {
            var data = await _genericQueryService.GetByIdAsync<NodeListDto>(id);
            _logger.LogInformation("api/node/GetById calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("node not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/node/GetById",
                    errors: "İstenilen node bulunamadı"
                    ).CreateResponseInstance();
            }
            return Response<NodeListDto>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
        //todo custom repository içerisine dahil edilecek
        [HttpGet("city/{cityId:guid}")]
        public async Task<IActionResult> GetByCityId(Guid cityId)
        {
            var data = await _context.Nodes.Where(x => x.CityId == cityId).FirstOrDefaultAsync();
            _logger.LogInformation("api/node/GetByCityId calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("node not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/node/GetByCityId",
                    errors: "İstenilen node bulunamadı"
                    ).CreateResponseInstance();
            }
            var map = _mapper.Map<NodeListDto>(data);
            return Response<NodeListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }

        //todo custom repository içerisine dahil edilecek
        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var data = await _context.Nodes.Where(x => x.Name == name).FirstOrDefaultAsync();
            _logger.LogInformation("api/node/GetByName calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("node not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/node/GetByName",
                    errors: "İstenilen node bulunamadı"
                    ).CreateResponseInstance();
            }
            var map = _mapper.Map<NodeListDto>(data);
            return Response<NodeListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NodeCreateDto dto)
        {
            dto.CreatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            var node = await _genericCommandService.AddAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/node/add calling enpoint");
            var map = _mapper.Map<NodeListDto>(node);
            return Response<NodeListDto>.Success(map, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpPut]
        public async Task<IActionResult> Update(NodeUpdateDto dto)
        {
            dto.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            await _genericCommandService.UpdateAsync(dto);
            await _genericCommandService.SaveChangesAsync();
            _logger.LogInformation($"api/node/update calling enpoint");
            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _genericQueryService.GetByIdAsync<DeleteDto>(id);
            data.UpdatedUserId = Guid.Parse(_sharedIdentityService.GetUserId);
            _logger.LogInformation("api/node/delete calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("node not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/node/delete",
                    errors: "İstenilen node bulunamadı"
                    ).CreateResponseInstance();
            }

            await _genericCommandService.RemoveAsync(data);
            await _genericCommandService.SaveChangesAsync();

            return Response<NoContent>.Success(StatusCodes.Status204NoContent).CreateResponseInstance();
        }
    }
}
