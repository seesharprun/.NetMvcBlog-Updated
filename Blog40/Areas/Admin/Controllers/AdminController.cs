using System.Web.Mvc;

namespace Blog40.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}