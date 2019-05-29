using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Retaguarda.Web.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    public class ClienteController : Controller
    {
        private readonly IClienteAppService appService;
        private readonly ITipoClienteAppService tipoClienteAppService;
        private readonly ITipoIdentificacaoAppService tipoIdentificacaoAppService;

        public ClienteController(
            IClienteAppService appService,
            ITipoClienteAppService tipoClienteAppService,
            ITipoIdentificacaoAppService tipoIdentificacaoAppService)
        {
            this.appService = appService;
            this.tipoClienteAppService = tipoClienteAppService;
            this.tipoIdentificacaoAppService = tipoIdentificacaoAppService;
        }

        [TemPermissao(FuncionalidadeEnum.Cliente, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = appService.Listar().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Cliente, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(string areaId)
        {
            var model = new ClienteViewModel()
            {
                Cadastro = DateTime.Today.ToString("dd/MM/yyyy"),
                SituacaoCadastral = true,
                TelaEmails = new EmailTelaViewModel()
                {
                    ApenasUmEmail = false,
                }
            };

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Cliente, AcaoEnum.Acessar)]
        public virtual IActionResult Visualizar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Cliente, AcaoEnum.Editar)]
        public virtual IActionResult Editar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Cliente, AcaoEnum.Cadastrar)]
        public virtual IActionResult Cadastrar(ClienteViewModel viewModel)
        {
            var resultado = appService.Inserir(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Cliente, AcaoEnum.Editar)]
        public virtual IActionResult Editar(ClienteViewModel viewModel)
        {
            var resultado = appService.Atualizar(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Cliente, AcaoEnum.Excluir)]
        public virtual IActionResult Excluir(string id)
        {
            var resultado = appService.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        private void PreencheCombosTela(ClienteViewModel model)
        {
            model.EstadosCivis = EnumExtensions.ToDictionary<EstadoCivilEnum>();
            model.Regioes = EnumExtensions.ToDictionary<RegiaoEnum>();
            model.Sexos = EnumExtensions.ToDictionary<SexoEnum>();
            model.TiposPessoa = EnumExtensions.ToDictionary<PessoaTipoEnum>();
            model.TelaTelefones.TiposTelefone = EnumExtensions.ToDictionary<TipoTelefoneEnum>();
            model.TiposCliente = tipoClienteAppService.RecuperarDropdown().Data;
            model.Ufs = EnumExtensions.ToDictionary<UfEnum>();
            model.TelaEnderecos.Ufs = model.Ufs;
            model.TelaIdentificacoes.TiposIdentificacao = tipoIdentificacaoAppService.RecuperarDropdown().Data;
        }
    }
}