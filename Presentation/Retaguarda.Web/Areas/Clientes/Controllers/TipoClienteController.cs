using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Retaguarda.Web.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    public class TipoClienteController : Controller
    {
        private readonly ITipoClienteAppService service;

        public TipoClienteController(ITipoClienteAppService service)
        {
            this.service = service;
        }

        [TemPermissao(FuncionalidadeEnum.TipoCliente, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = service.RecuperarTodos().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.TipoCliente, AcaoEnum.Acessar)]
        public IActionResult Visualizar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.TipoCliente, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(string areaId)
        {
            var model = new TipoClienteViewModel();
            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.TipoCliente, AcaoEnum.Editar)]
        public IActionResult Editar(string id)
        {
            var resultado = service.RecuperarPorId(id);

            var model = resultado.Data;

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoCliente, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(TipoClienteViewModel model)
        {
            var resultado = service.Inserir(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoCliente, AcaoEnum.Editar)]
        public IActionResult Editar(TipoClienteViewModel model)
        {
            var resultado = service.Atualizar(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.TipoCliente, AcaoEnum.Excluir)]
        public IActionResult Excluir(string id)
        {
            var resultado = service.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }
    }
}