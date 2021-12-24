using THH.IdentityServer.Dtos;
using THH.Shared.Core.ExtensionMethods;
using THH.Shared.Core.Response;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static Duende.IdentityServer.IdentityServerConstants;

namespace THH.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return 
                Response<IEnumerable<string>>
                .Success(roles.Select(x=>x.Name))
                .CreateResponseInstance();
        }

    }
}
