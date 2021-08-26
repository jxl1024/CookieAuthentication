using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuthorizeDemo.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录页面可以匿名访问
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            // 没有通过验证，将访问的网址保留下来
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.returnUrl = returnUrl;
            }
            return View();
        }

        /// <summary>
        /// 可以匿名访问
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="returnUrl">跳转的url</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(string loginName,string password,string returnUrl)
        {
            // 判断用户名和密码
            if (loginName == "admin" && password == "123456")
            {
                var claims = new Claim[]
                {
                    // 写入角色信息
                    new Claim(ClaimTypes.Role,"Leader"),
                    new Claim(ClaimTypes.Name,"jxl"),
                    new Claim(ClaimTypes.Sid,"1")
                };

                // 登录成功
                // 会把claims通过一定的加密存储到Cookie中，第二次访问的时候把Cookie带到服务端
                // 服务端把加密内容解密出来进行验证，有没有权限
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(claims, "claim")));
                // 如果returnUrl为空，则跳转到主页，否则跳转到原来访问的页面
                return new RedirectResult(string.IsNullOrEmpty(returnUrl) ? "/home/index" : returnUrl);
            }
            else
            {
                ViewBag.error = "用户名或密码错误";
                return View();
            }
        }
    }
}
