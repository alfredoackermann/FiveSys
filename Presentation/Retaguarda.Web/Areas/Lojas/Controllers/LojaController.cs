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
    [Area("Lojas")]
    public class LojaController : Controller
    {
        private readonly ILojaAppService appService;
        private readonly IRamoAppService ramoAppService;

        public LojaController(ILojaAppService appService, IRamoAppService RamoAppService)
        {
            this.appService = appService;
            this.ramoAppService = RamoAppService;
        }

        [TemPermissao(FuncionalidadeEnum.Loja, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = appService.Listar().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Loja, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar()
        {
            var model = new LojaViewModel()
            {
                Cadastro = DateTime.Today.ToString("dd/MM/yyyy"),
                TelaTelefones = new TelefoneTelaViewModel()
                {
                    ApenasUmTelefone = false
                },
                TelaEnderecos = new EnderecoTelaViewModel()
                {
                    ApenasUmEndereco = false
                },
                TelaEmails = new EmailTelaViewModel()
                {
                    ApenasUmEmail = false
                },

                SeriEnf = "1",
                SequenciaLnf = "1",
                SerieCte = "1",
                SequencialCte = "1",
                Numero = appService.ProximaLoja().PadLeft(3, '0')

            };

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Loja, AcaoEnum.Acessar)]
        public virtual IActionResult Visualizar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Loja, AcaoEnum.Editar)]
        public virtual IActionResult Editar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Loja, AcaoEnum.Cadastrar)]
        public virtual IActionResult Cadastrar(LojaViewModel viewModel)
        {
            var resultado = appService.Inserir(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Loja, AcaoEnum.Editar)]
        public virtual IActionResult Editar(LojaViewModel viewModel)
        {
            var resultado = appService.Atualizar(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Loja, AcaoEnum.Excluir)]
        public virtual IActionResult Excluir(string id)
        {
            var resultado = appService.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        private void PreencheCombosTela(LojaViewModel model)
        {
            model.Ramos = ramoAppService.RecuperarDropdown().Data;
            model.Crts = EnumExtensions.ToDictionary<CrtEnum>();
            model.Ufs = EnumExtensions.ToDictionary<UfEnum>();
            model.TelaTelefones.TiposTelefone = EnumExtensions.ToDictionary<TipoTelefoneEnum>();
            model.TelaEnderecos.Ufs = model.Ufs;


        }
    }
}