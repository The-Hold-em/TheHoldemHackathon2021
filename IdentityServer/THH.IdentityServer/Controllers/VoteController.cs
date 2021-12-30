using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

using THH.IdentityServer.Data;
using THH.IdentityServer.Models;
using THH.Shared.Core.ExtensionMethods;
using THH.Shared.Core.Response;

namespace THH.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VoteController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> FetchStatistics()
        {
            var statistic = new Statistic();
            var allUsers = await _context.Users.ToListAsync();
            statistic.TotalVoter = allUsers.Count;
            statistic.Voters=allUsers.Where(x=>x.IVoted).ToList().Count();
            return Response<Statistic>.Success(statistic).CreateResponseInstance();
        }
    }
}
