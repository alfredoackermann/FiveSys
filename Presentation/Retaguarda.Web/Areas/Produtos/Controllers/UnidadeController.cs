using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Retaguarda.Web.Areas.Admin.Controllers
{
    [Area("Produtos")]
    public class UnidadeController : Controller
    {
        private readonly IUnidadeAppService service;

        public UnidadeController(IUnidadeAppService service)
        {
            this.service = service;
        }

        [TemPermissao(FuncionalidadeEnum.Unidade, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = service.RecuperarTodos().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Unidade, AcaoEnum.Acessar)]
        public ActionResult Visualizar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Unidade, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(string areaId)
        {
            var model = new UnidadeViewModel();

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Unidade, AcaoEnum.Editar)]
        public ActionResult Editar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Unidade, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(UnidadeViewModel model)
        {
            var resultado = service.Inserir(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Unidade, AcaoEnum.Editar)]
        public ActionResult Editar(UnidadeViewModel model)
        {
            var resultado = service.Atualizar(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Unidade, AcaoEnum.Excluir)]
        public ActionResult Excluir(string id)
        {
            var resultado = service.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }
    }
}