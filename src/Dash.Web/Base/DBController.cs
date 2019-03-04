using Dash.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Dash.Web
{
    public class DBController : Controller
    {
        private DSDContext _db;
        public DSDContext Db => _db ?? (DSDContext)HttpContext?.RequestServices.GetService(typeof(DSDContext));
    }
}