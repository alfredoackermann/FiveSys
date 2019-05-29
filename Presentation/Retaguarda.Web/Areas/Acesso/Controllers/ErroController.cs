using Microsoft.AspNetCore.Mvc;

namespace Retaguarda.Web.Areas.Acesso.Controllers
{
    [Area("Acesso")]
    public class ErroController : Controller
    {
        public IActionResult AcessoNegado()
        {
            return View("403");
        }
    }
}