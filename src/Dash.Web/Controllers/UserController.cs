using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dash.Entity.Objects;
using Dash.Web.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dash.Web.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var account = await Db.Users.FirstOrDefaultAsync(z => z.Id == UserId);
            return Success(null, data: new {
                account.Firstname,
                account.Lastname,
                account.Email,
                account.Username
            });
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
                return Error(message: "Profile edit model parameters is invalid!", internalMessage: null, data: errors);
            }

            if(!string.IsNullOrEmpty(value.Password)){
                if(value.Password.Length < 6)
                    return Error("Password field must be min 6 character!");
                if(value.Password.Length > 20)
                return Error("Password field must be max 20 character!");
            }

            var hasUserDefinition = await Db.Users.AnyAsync(z => (z.Username.Equals(value.Username) || z.Email.Equals(value.Email)) && z.Id != UserId);
            if (hasUserDefinition)
                return Error(message: "This changes (username or email) is already using another record!");

            var user = await Db.Users.FirstOrDefaultAsync(z => z.Id == UserId);
            if(user == null)
                return Error(message: "User not found!");

            user.Firstname = value.Firstname;
            user.Lastname = value.Lastname;
            user.Email = value.Email;
            user.Username = value.Username;
            user.ModifiedOn = DateTime.UtcNow;

            if(!string.IsNullOrEmpty(value.Password))
                user.Password = new CryptoHelper().HashWithSHA512(value.Password);

            Db.Users.Update(user);
            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("User updated successfully.");
            else
                return Error(message: "Something is wrong with your model!");
        }
    }
}