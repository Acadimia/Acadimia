using Acadimia.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Acadimia.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
