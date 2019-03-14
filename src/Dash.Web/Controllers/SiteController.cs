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

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] DSRMAdd value){

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

            var accessCode = HashWithSHA512(Guid.NewGuid() + DateTime.Now.ToString());
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
            if(result > 0)
                return Success("Site saved successfully.", data: new {
                    Id = data.Id
                });
            else
                return Error(message: "Something is wrong with your model!");
        }

        private string HashWithSHA512(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);

            return Convert.ToBase64String(result);
        }
    }
}