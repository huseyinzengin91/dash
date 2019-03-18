using Dash.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Dash.Web
{
    public class DBController : Controller
    {
        public DSDContext Db => (DSDContext)HttpContext?.RequestServices.GetService(typeof(DSDContext));
    }
}