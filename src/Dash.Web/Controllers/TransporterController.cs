using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Dash.Web.Models.Transporter;
using System.Collections.Generic;
using Dash.Entity.Objects;
using System;
using System.Text;
using System.Security.Cryptography;

namespace Dash.Web.Controllers
{
    public class TransporterController : ApiController
    {
        [AllowAnonymous]
        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody]DSRMTransportRequest value)
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

            if (fromSite.AccessCodeEndDate < DateTime.UtcNow)
                return Error(message: "Site definition is expired!");

            var checkAccesible = await (from acs in Db.AccessibleSites
                      join s in Db.Sites on acs.SiteId equals s.Id
                      join ss in Db.Sites on acs.AccessibleSiteId equals ss.Id
                      where
                          s.Id == fromSite.Id &&
                          ss.SiteAddress.Equals(value.ToSiteAddress)
                      select acs).AnyAsync();

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
                    OwnerSiteId = fromSite.Id,
                    DataShareCode = shareCode,
                    Status = DSDDataShareStatusTypes.Active,
                    Value = value.Data
                });

                await Db.SaveChangesAsync();

                return Success(message: "Send request is successfult created.", data: new { DataShareCode = System.Net.WebUtility.UrlEncode(shareCode) });
            }
            catch (System.Exception ex)
            {
                return Error(message: "Send request unable to create!", internalMessage: ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery]DSRMGetTransportData value)
        {
            if (!ModelState.IsValid)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Any())
                        errors.Add(item.Key, string.Join("; ", item.Value.Errors.Select(x => x.ErrorMessage)));
                }

                return Error(message: "Get transport data model parameters is invalid!", internalMessage: null, data: errors);
            }

            var fromSite = await Db.Sites.FirstOrDefaultAsync(z => z.SiteAddress.Equals(value.FromSiteAddress) && z.AccessCode.Equals(value.AccessCode));

            if (fromSite == null)
                return Error(message: "Site definition not found!");

            if (fromSite.Status == DSDSiteStatusTypes.Deactive)
                return Error(message: "Site definition is deactived!");

            if (fromSite.AccessCodeEndDate < DateTime.UtcNow)
                return Error(message: "Site definition is expired!");

            var dataShare = await Db.DataShares.FirstOrDefaultAsync(z => z.DataShareCode.Equals(value.DataShareCode));

            if (dataShare == null)
                return Error(message: "Data share definition not found!");

            if (dataShare.Status == DSDDataShareStatusTypes.Deactive)
                return Error(message: "Data share definition is deactived!");

            if (dataShare.ExpireDate < DateTime.UtcNow)
                return Error(message: "Data share definition is expired!");


            var checkAccesible = await (from acs in Db.AccessibleSites
                      where
                          acs.SiteId == dataShare.OwnerSiteId &&
                          acs.AccessibleSiteId == fromSite.Id
                      select acs).AnyAsync();

            if (!checkAccesible){
                var ownerSiteAddress = await Db.Sites.FirstOrDefaultAsync(z => z.Id == dataShare.OwnerSiteId);
                return Error(message: $"You have not access permission {ownerSiteAddress} to {value.FromSiteAddress}");
            }
                

            try
            {
                dataShare.Status = DSDDataShareStatusTypes.Deactive;
                var x= Db.DataShares.Update(dataShare);
                await Db.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                return Error(message: "Transport data status unable to update!", internalMessage: ex.Message);
            }

            return Success(message: null, data: new { Data = dataShare.Value });
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