using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dash.Entity.Objects;
using System.Security.Cryptography;
using System.Text;

namespace Dash.Web.Controllers
{
    public class AuthController : DBController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttemptLogin(UserLoginModel req){

            if(!ModelState.IsValid)
                return View("Index");
            
            var userRecord = await this.Db.Users.FirstOrDefaultAsync(z => z.Username.Equals(req.Username) && z.Password.Equals(new CryptoHelper().HashWithSHA512(req.Password)));
            if(userRecord == null){
                ModelState.AddModelError("UserNotFound", "User not found!");
                return View("Index");
            }

            if(userRecord.Status != DSDUserStatusTypes.Active){
                ModelState.AddModelError("UserNotActivated", "User not activated!");
                return View("Index");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userRecord.Id.ToString()),
                new Claim(ClaimTypes.Name, userRecord.Firstname + " " +userRecord.Lastname),
                new Claim(ClaimTypes.Email, userRecord.Email)
            };

            //Create the principal user from the claims
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties() { IsPersistent = false };

            //Ask MVC to create the auth cookie and store it
            await this.HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                     principal, authenticationProperties);

            return RedirectToAction("Index", "Home");
        }
    
        public async Task<IActionResult> SignOut()
        {
            await this.HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }

    

    public class UserLoginModel
    {
        [Required]
        [StringLength(10)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string Password { get; set; }
    }
}