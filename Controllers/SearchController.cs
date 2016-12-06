using auth0search.Models;
using auth0search.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace auth0search.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpPost]
        public IActionResult Search([FromBody]SearchPayload payload)
        {
            return Json(_searchService.Search("users",payload));
        }
        
        [HttpGet]
        public IActionResult Suggest(string term, bool fuzzy = true)
        {
            var response = _searchService.Suggest("users", "emails", term, fuzzy); 
            return Json(response);  
        }

    }
}
