using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dash.Entity.Objects;
using Dash.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SetupController : ApiController
{
    [HttpGet("Run")]
    [AllowAnonymous]
    public async Task<IActionResult> Run()
    {
        var userExist = await Db.Users.AnyAsync();
        if (!userExist)
        {
            await Db.Users.AddAsync(new DSDUser
            {
                Id = Guid.NewGuid(),
                Firstname = "Admin",
                Lastname = "Admin",
                Email = "admin@admin.com",
                Password = new CryptoHelper().HashWithSHA512("123456"),
                Username = "admin",
                Status = DSDUserStatusTypes.Active,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            });
            await Db.SaveChangesAsync();
        }

        return Success("Setup is successfully");
    }

    
}