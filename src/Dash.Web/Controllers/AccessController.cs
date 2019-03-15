using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dash.Entity.Objects;
using Dash.Web.Models.Access;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dash.Web.Controllers
{
    public class AccessController : ApiController
    {
        [HttpGet("AccessList")]
        public async Task<IActionResult> AccessList()
        {
            var accessList = await (from acs in Db.AccessibleSites
                                    join site in Db.Sites on acs.SiteId equals site.Id
                                    join accessibleSite in Db.Sites on acs.AccessibleSiteId equals accessibleSite.Id
                                    select new
                                    {
                                        Id = acs.Id,
                                        SiteId = site.Id,
                                        SiteName = site.Name,
                                        SiteAddress = site.SiteAddress,
                                        AccessibleSiteId = accessibleSite.Id,
                                        AccessibleSiteName = accessibleSite.Name,
                                        AccessibleSiteAddress = accessibleSite.SiteAddress,
                                        CreatedOn = acs.CreatedOn,
                                        ModifiedOn = acs.ModifiedOn
                                    }).ToListAsync();

            return Success(null, accessList);
        }

        [HttpGet("GetSites")]
        public async Task<IActionResult> GetSites()
        {
            var sites = await Db.Sites.Where(z => z.Status == DSDSiteStatusTypes.Active).ToListAsync();
            var list = sites.Select(z =>
                    new
                    {
                        z.Id,
                        z.Name,
                        z.SiteAddress,
                    }).ToList();

            return Success(null, list);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] DSRMAdd value)
        {

            if (!ModelState.IsValid)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Any())
                        errors.Add(item.Key, string.Join("; ", item.Value.Errors.Select(x => x.ErrorMessage)));
                }

                return Error(message: "Accessible Site add model parameters is invalid!", internalMessage: null, data: errors);
            }

            var hasAccessibleSiteDefinition = await Db.AccessibleSites.AnyAsync(z => z.SiteId == value.SiteId && z.AccessibleSiteId == z.AccessibleSiteId);

            if (hasAccessibleSiteDefinition)
                return Error(message: "Accessible Site definition is already existing!");

            var data = new DSDAccessibleSite
            {
                Id = Guid.NewGuid(),
                SiteId = value.SiteId,
                AccessibleSiteId = value.AccessibleSiteId
            };
            Db.AccessibleSites.Add(data);

            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("Accessible Site saved successfully.", data: new
                {
                    Id = data.Id
                });
            else
                return Error(message: "Something is wrong with your model!");
        }
    }
}