using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Retaguarda.Web.Areas.Produtos.Controllers
{
    [Area("Produtos")]
    public class FornecedorController : Controller
    {
        private readonly IFornecedorAppService appService;

        public FornecedorController(IFornecedorAppService appService)
        {
            this.appService = appService;
        }

        [TemPermissao(FuncionalidadeEnum.Fornecedor, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = appService.Listar().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Fornecedor, AcaoEnum.Cadastrar)]
        public IActionResult Cadastrar(string areaId)
        {
            var model = new FornecedorViewModel()
            {
                Cadastro = DateTime.Today.ToString("dd/MM/yyyy"),
                TelaTelefones = new TelefoneTelaViewModel()
                {
                    ApenasUmTelefone = false
                },
                TelaEnderecos = new EnderecoTelaViewModel()
                {
                    ApenasUmEndereco = true
                },
                TelaEmails = new EmailTelaViewModel()
                {
                    ApenasUmEmail = false
                },
                Numero = appService.ProximoFornecedor().PadLeft(3, '0')
            };

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Fornecedor, AcaoEnum.Acessar)]
        public virtual IActionResult Visualizar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.TelaEnderecos.ApenasUmEndereco = true;
            model.Numero = model.Numero.PadLeft(3, '0');
            model.SomenteLeitura = true;

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Fornecedor, AcaoEnum.Editar)]
        public virtual IActionResult Editar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.Numero = model.Numero.PadLeft(3, '0');
            model.TelaEnderecos.ApenasUmEndereco = true;
            PreencheCombosTela(model);

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Fornecedor, AcaoEnum.Cadastrar)]
        public virtual IActionResult Cadastrar(FornecedorViewModel viewModel)
        {
            var resultado = appService.Inserir(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Fornecedor, AcaoEnum.Editar)]
        public virtual IActionResult Editar(FornecedorViewModel viewModel)
        {
            var resultado = appService.Atualizar(viewModel);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Fornecedor, AcaoEnum.Excluir)]
        public virtual IActionResult Excluir(string id)
        {
            var resultado = appService.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        private void PreencheCombosTela(FornecedorViewModel model)
        {
            model.TiposPessoa = EnumExtensions.ToDictionary<PessoaTipoEnum>();
            model.TelaTelefones.TiposTelefone = EnumExtensions.ToDictionary<TipoTelefoneEnum>();
            model.Ufs = EnumExtensions.ToDictionary<UfEnum>();
            model.TelaEnderecos.Ufs = model.Ufs;
            model.Sexos = EnumExtensions.ToDictionary<SexoEnum>();
            model.EstadosCivis = EnumExtensions.ToDictionary<EstadoCivilEnum>();
        }
    }
}