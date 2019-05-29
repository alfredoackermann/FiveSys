using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiveSys.Presentation.Retaguarda.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            if (base.HttpContext.Session.GetString("Usuario") == null)
                return RedirectPermanent(Url.Action("Index", "Home", new { area = "Acesso", timestamp = TimeSpan.FromMilliseconds(1).ToString() }));

            return View();
        }
    }
}