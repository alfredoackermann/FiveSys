using System;
using System.Linq;
using FileSys.Retaguarda.Application.Acesso.ViewModel;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FiveSys.Presentation.Retaguarda.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static bool TemPermissao(this IHtmlHelper html, FuncionalidadeEnum funcionalidade, AcaoEnum acao)
        {
            return VerificaPermissao(html.ViewContext.HttpContext, funcionalidade, acao);
        }

        public static bool SenhaTemporaria(this IHtmlHelper html)
        {
            bool autorizado = false;
            if (html.ViewContext.HttpContext.Session.IsAvailable)
            {
                var dados = html.ViewContext.HttpContext.Session.GetString("Usuario");
                if (dados != null)
                {
                    var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(dados);

                    if (usuario != null)
                    {
                        return usuario.SenhaTemporaria;
                    }
                }
            }

            return autorizado;
        }

        public static string IsSelected(this IHtmlHelper html, string area = null, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            string currentArea = (string)html.ViewContext.RouteData.Values["area"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (String.IsNullOrEmpty(area))
                area = currentArea;

            return area?.ToLower() == currentArea?.ToLower() &&
                controller.ToLower() == currentController.ToLower() &&
                action.ToLower() == currentAction.ToLower() ? cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static bool VerificaPermissao(HttpContext httpContext, FuncionalidadeEnum funcionalidade, AcaoEnum acao)
        {
            bool autorizado = false;
            if (httpContext.Session.IsAvailable)
            {
                var dados = httpContext.Session.GetString("Perfil");
                if (dados != null)
                {
                    var perfil = JsonConvert.DeserializeObject<PerfilViewModel>(dados);

                    if (perfil != null)
                    {
                        var funcionalide = perfil.Permissoes.SingleOrDefault(x => x.Funcionalidade == funcionalidade);

                        if (funcionalide != null)
                            autorizado = ((funcionalide.Acoes & (int)acao) == (int)acao);
                    }
                }
            }

            return autorizado;
        }
    }
}
