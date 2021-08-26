using CookieAuthorizeDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAuthorizeDemo.Controllers
{
    [Authorize(Roles = "Admin,Leader,Manager")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Admin,Leader,Manager三个角色中的任何一个角色都可以访问Index方法
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Leader,Manager这两个角色中的任何一个角色都可以访问Index方法
        /// Controller和Action上面都设置了Authorize，以Action上面设置的起作用
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
