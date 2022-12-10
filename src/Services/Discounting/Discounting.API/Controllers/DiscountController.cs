using Microsoft.AspNetCore.Mvc;

namespace Discounting.API.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
