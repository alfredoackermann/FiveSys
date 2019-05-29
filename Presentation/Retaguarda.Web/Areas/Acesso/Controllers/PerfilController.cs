using FileSys.Retaguarda.Application.Acesso.Interface;
using FileSys.Retaguarda.Application.Acesso.ViewModel;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Presentation.Retaguarda.Web.Helpers;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Retaguarda.Web.Areas.Acesso.Controllers
{
    [Area("Acesso")]
    public class PerfilController : Controller
    {
        private readonly IPerfilAppService appService;

        public PerfilController(IPerfilAppService appService)
        {
            this.appService = appService;
        }

        [TemPermissao(FuncionalidadeEnum.Perfil, AcaoEnum.Acessar)]
        public IActionResult Index()
        {
            var resultado = appService.RecuperarTodos().Data;

            return View(resultado);
        }

        [TemPermissao(FuncionalidadeEnum.Perfil, AcaoEnum.Acessar)]
        public ActionResult Visualizar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;
            model.SomenteLeitura = true;

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Perfil, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(string areaId)
        {
            var model = new PerfilViewModel();

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [TemPermissao(FuncionalidadeEnum.Perfil, AcaoEnum.Editar)]
        public ActionResult Editar(string id)
        {
            var resultado = appService.RecuperarPorId(id);

            var model = resultado.Data;

            PreencheCombosTela(model);

            return View("Form", model);
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Perfil, AcaoEnum.Cadastrar)]
        public ActionResult Cadastrar(IFormCollection formCollection, PerfilViewModel model)
        {
            PreencherPerfilFuncionalidade(formCollection, model);

            var resultado = appService.Inserir(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Perfil, AcaoEnum.Editar)]
        public ActionResult Editar(IFormCollection formCollection, PerfilViewModel model)
        {
            PreencherPerfilFuncionalidade(formCollection, model);

            var resultado = appService.Atualizar(model);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        [HttpPost]
        [TemPermissao(FuncionalidadeEnum.Perfil, AcaoEnum.Excluir)]
        public ActionResult Excluir(string id)
        {
            var resultado = appService.RemoverPorId(id);

            if (resultado.Successo)
                resultado.RedirecionarPara(Url.Action(nameof(Index)));

            return Json(resultado.Retorno());
        }

        private void PreencherPerfilFuncionalidade(IFormCollection formCollection, PerfilViewModel viewModel)
        {
            var permissoes = new List<PermissaoViewModel>();

            var funcionalidades = EnumExtensions.ToDictionary<FuncionalidadeEnum>();

            foreach (var item in funcionalidades)
            {
                var actions = formCollection[item.Key];
                if (actions.Count > 0)
                {
                    permissoes.Add(new PermissaoViewModel()
                    {
                        Funcionalidade = EnumExtensions.ToEnum<FuncionalidadeEnum>(item.Key),
                        Acoes = actions.Sum(x => int.Parse(x))
                    });
                }
            }

            viewModel.Permissoes = permissoes;
        }

        private void PreencheCombosTela(PerfilViewModel model)
        {
            model.Funcionalidades = EnumExtensions.ToDictionary<FuncionalidadeEnum>();
            model.Acoes = EnumExtensions.ToDictionary<AcaoEnum>();
        }
    }
}