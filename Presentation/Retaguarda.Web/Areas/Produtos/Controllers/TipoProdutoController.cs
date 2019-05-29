using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Retaguarda.Web.Areas.Produtos.Controllers
{
    [Area("Produtos")]
    public class TipoProdutoController : Controller
    {
        private readonly ITipoProdutoAppService service;

        public TipoProdutoController(ITipoProdutoAppService service)
        {
            this.service = service;
        }

        [TemPermissao(FuncionalidadeEnum.TipoProduto, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = service.RecuperarTodos().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.TipoProduto, AcaoEnum.Acessar)]
        public ActionResult Visualizar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.TipoProduto, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(string areaId)
        {
            var model = new TipoProdutoViewModel();

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.TipoProduto, AcaoEnum.Editar)]
        public ActionResult Editar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoProduto, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(TipoProdutoViewModel model)
        {
            var resultado = service.Inserir(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoProduto, AcaoEnum.Editar)]
        public ActionResult Editar(TipoProdutoViewModel model)
        {
            var resultado = service.Atualizar(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoProduto, AcaoEnum.Excluir)]
        public ActionResult Excluir(string id)
        {
            var resultado = service.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }
    }
}