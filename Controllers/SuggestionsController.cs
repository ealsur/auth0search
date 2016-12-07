using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace auth0search.Controllers
{
    [Authorize]
    public class SuggestionsController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
