using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubJumana.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult hi()
        {
            return View();
        }
        [HttpPost]
        public IActionResult hi(ICollection<Book> books)
        {
            var rrr = books.ToList();
            return View();
        }

        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime DatePublished { get; set; }
        }
    }
}