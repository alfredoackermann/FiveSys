using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Retaguarda.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RamoController : Controller
    {
        private readonly IRamoAppService service;

        public RamoController(IRamoAppService service)
        {
            this.service = service;
        }

        [TemPermissao(FuncionalidadeEnum.Ramo, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = service.RecuperarTodos().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Ramo, AcaoEnum.Acessar)]
        public ActionResult Visualizar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Ramo, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(string areaId)
        {
            var model = new RamoViewModel();

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Ramo, AcaoEnum.Editar)]
        public ActionResult Editar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Ramo, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(RamoViewModel model)
        {
            var resultado = service.Inserir(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Ramo, AcaoEnum.Editar)]
        public ActionResult Editar(RamoViewModel model)
        {
            var resultado = service.Atualizar(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Ramo, AcaoEnum.Excluir)]
        public ActionResult Excluir(string id)
        {
            var resultado = service.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }
    }
}