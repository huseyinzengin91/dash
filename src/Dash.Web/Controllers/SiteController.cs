using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dash.Web.Controllers
{
    public class SiteController : ApiController
    {
        [HttpGet("GetSites")]
        public async Task<IActionResult> GetSites()
        {
            var sites = await Db.Sites.ToListAsync();
            var list = sites.Select(x =>
                    new
                    {
                        x.Id,
                        x.Name,
                        x.SiteAddress,
                        x.Status,
                        x.AccessCode,
                        x.AccessCodeEndDate,
                        x.CreatedOn,
                        x.ModifiedOn,
                        StatusText = x.Status.ToString()
                    }).ToList();

            return Success(null, list);
        }
    }
}