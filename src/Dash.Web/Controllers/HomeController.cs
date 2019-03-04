using Microsoft.AspNetCore.Mvc;

namespace Dash.Web.Controllers
{
    public class HomeController : SecureController
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
