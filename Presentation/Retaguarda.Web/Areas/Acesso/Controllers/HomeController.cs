using System;
using FileSys.Retaguarda.Application.Acesso.Interface;
using FileSys.Retaguarda.Application.Acesso.ViewModel;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Retaguarda.Web.Areas.Acesso.Controllers
{
    [Area("Acesso")]
    public class HomeController : Controller
    {
        private readonly IUsuarioAppService appService;
        private readonly IPerfilAppService perfilAppService;

        public HomeController(IUsuarioAppService appService,
            IPerfilAppService perfilAppService)
        {
            this.appService = appService;
            this.perfilAppService = perfilAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(LoginViewModel viewModel)
        {
            var resultado = appService.ValidarLogin(viewModel);

            if (resultado.Successo)
            {
                var usuario = resultado.Data;

                base.HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));

                if (usuario.SenhaTemporaria)
                {
                    resultado.RedirecionarPara(Url.Action("Index", "Home", new { area = "Acesso", timestamp = TimeSpan.FromMilliseconds(1).ToString() }));
                }
                else
                {
                    var perfil = perfilAppService.RecuperarPorId(usuario.PerfilId).Data;

                    base.HttpContext.Session.SetString("Perfil", JsonConvert.SerializeObject(perfil));

                    resultado.RedirecionarPara(Url.Action("Index", "Home", new { area = "" }));
                }
            }

            return Json(resultado.Retorno());
        }

        [HttpPost]
        public IActionResult TrocarSenha(TrocarSenhaViewModel viewModel)
        {
            var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(base.HttpContext.Session.GetString("Usuario"));

            viewModel.Login = usuario.Login;

            var resultado = appService.TrocarSenha(viewModel);

            if (resultado.Successo)
            {
                usuario = resultado.Data;

                base.HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));

                var perfil = perfilAppService.RecuperarPorId(usuario.PerfilId).Data;

                base.HttpContext.Session.SetString("Perfil", JsonConvert.SerializeObject(perfil));

                resultado.RedirecionarPara(Url.Action("Index", "Home", new { area = ""}));
            }

            return Json(resultado.Retorno());
        }
    }
}