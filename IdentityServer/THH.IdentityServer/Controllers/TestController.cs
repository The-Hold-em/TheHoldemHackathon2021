using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using static Mernis.KPSPublicSoapClient;

namespace THH.IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController:ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            using var service = new Mernis.KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);
            Mernis.TCKimlikNoDogrulaResponse status = await service.TCKimlikNoDogrulaAsync(40711843626, "MELEK", "KAMA", 2000);
            return Ok(status.Body.TCKimlikNoDogrulaResult);
        }
    }
}
