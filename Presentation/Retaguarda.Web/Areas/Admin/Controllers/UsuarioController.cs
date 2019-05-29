using FileSys.Retaguarda.Application.Acesso.Interface;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Retaguarda.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService appService;
        private readonly IPerfilAppService perfilAppService;

        public UsuarioController(IUsuarioAppService appService,
            IPerfilAppService perfilAppService)
        {
            this.appService = appService;
            this.perfilAppService = perfilAppService;
        }

        [TemPermissao(FuncionalidadeEnum.Usuario, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = appService.RecuperarTodos().Data;
            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Usuario, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(string areaId)
        {
            var model = new UsuarioViewModel()
            {
                Admissao = DateTime.Today.ToString("dd/MM/yyyy"),
                TelaEmails = new EmailTelaViewModel()
                {
                    ApenasUmEmail = true,
                }
            };

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Usuario, AcaoEnum.Acessar)]
        public virtual IActionResult Visualizar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;
            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Usuario, AcaoEnum.Editar)]
        public virtual IActionResult Editar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            PreencheCombosTela(model);

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Usuario, AcaoEnum.Cadastrar)]
        public virtual IActionResult Cadastrar(UsuarioViewModel viewModel)
        {
            var resultado = appService.Inserir(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Usuario, AcaoEnum.Editar)]
        public virtual IActionResult Editar(UsuarioViewModel viewModel)
        {
            var resultado = appService.Atualizar(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Usuario, AcaoEnum.Excluir)]
        public virtual IActionResult Excluir(string id)
        {
            var resultado = appService.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        private void PreencheCombosTela(UsuarioViewModel model)
        {
            model.Situacao = EnumExtensions.ToDictionary<StatusEnum>();
            model.Perfis = perfilAppService.RecuperarDropdown().Data;
        }
    }
}