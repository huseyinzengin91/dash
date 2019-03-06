using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Dash.Web.Models.Client;
using System.Collections.Generic;
using Dash.Entity.Objects;
using System;
using System.Text;
using System.Security.Cryptography;

namespace Dash.Web.Controllers
{
    public class TransporterController : ApiController
    {
        [HttpPost("Send")]
        [AllowAnonymous]
        public async Task<IActionResult> SendRequest([FromBody]DSVMTransportRequest value)
        {
            if (!ModelState.IsValid)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Any())
                        errors.Add(item.Key, string.Join("; ", item.Value.Errors.Select(x => x.ErrorMessage)));
                }

                return Error(message: "Transport request model parameters is invalid!", internalMessage: null, data: errors);
            }

            var fromSite = await Db.Sites.FirstOrDefaultAsync(z => z.SiteAddress.Equals(value.FromSiteAddress) && z.AccessCode.Equals(value.AccessCode));

            if (fromSite == null)
                return Error(message: "Site definition not found!");

            if (fromSite.Status == DSDSiteStatusTypes.Deactive)
                return Error(message: "Site definition is deactived!");

            if (fromSite.AccessCodeEndDate < DateTime.Now)
                return Error(message: "Site definition is expired!");

            var checkAccesible = await Db.AccessibleSites.AnyAsync(z => z.Site.Id == fromSite.Id && z.AccessibleSite.SiteAddress.Equals(value.ToSiteAddress));

            if (!checkAccesible)
                return Error(message: $"You have not access permission to {value.ToSiteAddress}");

            try
            {
                var shareCode = HashWithSHA512(Guid.NewGuid() + DateTime.Now.ToString());
                var addResult = await Db.DataShares.AddAsync(new DSDDataShare
                {
                    Id = new Guid(),
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    ExpireDate = DateTime.UtcNow.AddMinutes(5),
                    OwnerSite = new DSDSite { Id = fromSite.Id },
                    DataShareCode = shareCode,
                    Status = DSDDataShareStatusTypes.Active,
                    Value = value.Data
                });

                await Db.SaveChangesAsync();

                return Success(message: "Send request is successfult created.", data: new { ShareCode = shareCode });
            }
            catch (System.Exception ex)
            {
                return Error(message: "Send request unable to create!", internalMessage: ex.Message);
            }

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