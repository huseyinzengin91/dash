using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dash.Entity.Objects;
using Dash.Web.Models.Site;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dash.Web.Controllers
{
    public class SiteController : ApiController
    {
        [HttpGet("GetSites")]
        public async Task<IActionResult> GetSites()
        {
            var sites = await Db.Sites.OrderByDescending(z => z.CreatedOn).ToListAsync();
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

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var site = await Db.Sites.FirstAsync(z => z.Id == id);

            if (site == null)
                return Error("Site definition not found!");

            return Success(null, new
            {
                site.Id,
                site.Name,
                site.Description,
                site.SiteAddress,
                site.Status,
                site.AccessCode,
                site.AccessCodeEndDate,
            });
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

                return Error(message: "Site add model parameters is invalid!", internalMessage: null, data: errors);
            }

            var hasSiteDefinition = await Db.Sites.AnyAsync(z => z.Name.Equals(value.Name) || z.SiteAddress.Equals(value.SiteAddress));

            if (hasSiteDefinition)
                return Error(message: "Site definition is already existing!");

            var accessCode = new CryptoHelper().HashWithSHA512(Guid.NewGuid() + DateTime.Now.ToString());
            var data = new DSDSite
            {
                AccessCode = accessCode,
                AccessCodeEndDate = value.ExpirationDate,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Name = value.Name,
                Description = value.Description,
                SiteAddress = value.SiteAddress,
                Status = (DSDSiteStatusTypes)value.Status,
                Id = Guid.NewGuid()
            };

            Db.Sites.Add(data);
            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("Site saved successfully.", data: new
                {
                    Id = data.Id
                });
            else
                return Error(message: "Something is wrong with your model!");
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] DSRMEdit value)
        {
            if (!ModelState.IsValid)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Any())
                        errors.Add(item.Key, string.Join("; ", item.Value.Errors.Select(x => x.ErrorMessage)));
                }
                return Error(message: "Site edit model parameters is invalid!", internalMessage: null, data: errors);
            }

            var hasSiteDefinition = await Db.Sites.AnyAsync(z => (z.Name.Equals(value.Name) || z.SiteAddress.Equals(value.SiteAddress)) && z.Id != value.Id);

            if (hasSiteDefinition)
                return Error(message: "This changes is already using another record!");

            var site = await Db.Sites.FirstOrDefaultAsync(z => z.Id == value.Id);

            if(site == null)
                return Error(message: "Site definition not found!");

            site.AccessCode = value.AccessCode;
            site.AccessCodeEndDate = value.ExpirationDate;
            site.ModifiedOn = DateTime.UtcNow;
            site.Name = value.Name;
            site.Description = value.Description;
            site.SiteAddress = value.SiteAddress;
            site.Status = (DSDSiteStatusTypes)value.Status;

            Db.Sites.Update(site);
            
            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("Site saved successfully.");
            else
                return Error(message: "Something is wrong with your model!");
        }
        
        [HttpGet("NewHash")]
        public IActionResult NewHash(){
            var accessCode = new CryptoHelper().HashWithSHA512(Guid.NewGuid() + DateTime.Now.ToString());
            return Success(null, data: new { accessCode });
        }
    }
}