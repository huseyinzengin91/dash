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

namespace Dash.Web.Controllers
{
    public class SampleController : ApiController
    {
        public IActionResult Index()
        {
            MyUser myUser = new MyUser(){
                Id = Convert.ToInt32((HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity).FindFirst(z => z.Type == ClaimTypes.NameIdentifier).Value),
                Name = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity).FindFirst(z => z.Type == ClaimTypes.Name).Value
            };
            
            return View(myUser);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {

            bool exist = await this.Db.Users.AnyAsync(z => z.Username.Equals("huseyin") && z.Password.Equals("1234") && z.Status == DSDUserStatusTypes.Active);
            var users = await this.Db.Users.FirstAsync();
            //Create a plain C# Class
            //In real-life, get this from a database after verifying the username and password
            MyUser user = new MyUser()
            {
                Id = 1,
                Name = "Fred Fish"
            };

            //Add the ID of the user and the name to a collection of Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            //Create the principal user from the claims
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties() { IsPersistent = false };

            this.HttpContext.SignOutAsync();

            //Ask MVC to create the auth cookie and store it
            await this.HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                     principal, authenticationProperties);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }

    public class MyUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
