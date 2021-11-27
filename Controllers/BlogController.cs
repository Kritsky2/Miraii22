using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Miraii.Models;

namespace Miraii.Controllers
{
    public class BlogController : Controller
    {
        [Route("blog")]
        public IActionResult Index()
        {
            //get all blogs
            return View();
        }

        [Route("blog/{id}")]
        public IActionResult Blog(int id)
        {
            //get the selected blog
            return View();
        }
    }
}
