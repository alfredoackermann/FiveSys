using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Retaguarda.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParametroController : Controller
    {
        private readonly IParametroAppService appService;

        public ParametroController(
            IParametroAppService appService)
        {
            this.appService = appService;
        }

        [TemPermissao(FuncionalidadeEnum.Parametro, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = appService.RecuperarTodos().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Parametro, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(string areaId)
        {
            var model = new ParametroViewModel()
            {
                Tipos = EnumExtensions.ToDictionary<ParametroTipoEnum>()
            };

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Parametro, AcaoEnum.Acessar)]
        public virtual IActionResult Visualizar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;
            model.Tipos = EnumExtensions.ToDictionary<ParametroTipoEnum>();

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Parametro, AcaoEnum.Editar)]
        public virtual IActionResult Editar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.Tipos = EnumExtensions.ToDictionary<ParametroTipoEnum>();

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Parametro, AcaoEnum.Cadastrar)]
        public virtual IActionResult Cadastrar(ParametroViewModel viewModel)
        {
            var resultado = appService.Inserir(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Parametro, AcaoEnum.Editar)]
        public virtual IActionResult Editar(ParametroViewModel viewModel)
        {
            var resultado = appService.Atualizar(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Parametro, AcaoEnum.Excluir)]
        public virtual IActionResult Excluir(string id)
        {
            var resultado = appService.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }
    }
}