using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using THH.Services.MainApi.Entities.Concrete;
using THH.Shared.Core.ExtensionMethods;
using THH.Shared.Core.Response;
using THH.Shared.DAL.Interfaces;

namespace THH.Services.MainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IGenericQueryRepository<City> _genericQueryRepository;
        private readonly ILogger<CityController> _logger;

        public CityController(IGenericQueryRepository<City> genericQueryRepository,ILogger<CityController> logger)
        {
            _genericQueryRepository = genericQueryRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _genericQueryRepository.GetAllAsync();
            _logger.LogInformation("api/city/getall calling enpoint");
            return Response<IEnumerable<City>>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _genericQueryRepository.GetByIdAsync(id);
            _logger.LogInformation("api/city/GetById calling enpoint");
            if (data.IsNull())
            {
                _logger.LogInformation("node not found");
                return Response<NoContent>.Fail(
                    statusCode: StatusCodes.Status404NotFound,
                    isShow: true,
                    path: "api/city/GetById",
                    errors: "İstenilen city bulunamadı"
                    ).CreateResponseInstance();
            }
            return Response<City>.Success(data, StatusCodes.Status200OK).CreateResponseInstance();
        }
    }
}
