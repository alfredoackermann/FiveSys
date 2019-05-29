using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Retaguarda.Web.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    public class TipoIdentificacaoController : Controller
    {
        private readonly ITipoIdentificacaoAppService service;

        public TipoIdentificacaoController(ITipoIdentificacaoAppService service)
        {
            this.service = service;

        }

        [TemPermissao(FuncionalidadeEnum.TipoIdentificacao, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = service.RecuperarTodos().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.TipoIdentificacao, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(string areaId)
        {
            var model = new TipoIdentificacaoViewModel();
            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.TipoIdentificacao, AcaoEnum.Acessar)]
        public IActionResult Visualizar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.TipoIdentificacao, AcaoEnum.Editar)]
        public IActionResult Editar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoIdentificacao, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(TipoIdentificacaoViewModel model)
        {
            var resultado = service.Inserir(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoIdentificacao, AcaoEnum.Editar)]
        public IActionResult Editar(TipoIdentificacaoViewModel model)
        {
            var resultado = service.Atualizar(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoIdentificacao, AcaoEnum.Excluir)]
        public IActionResult Excluir(string id)
        {
            var resultado = service.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }
    }
}