using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dash.Web
{
    [Authorize]
    public class SecureController : DBController
    {
        public Guid UserId {
            get => new Guid((HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity).FindFirst(z => z.Type == ClaimTypes.NameIdentifier).Value);
        }
        //Name = (HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity).FindFirst(z => z.Type == ClaimTypes.Name).Value
    }
}