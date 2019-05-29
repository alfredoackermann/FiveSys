using System.Threading.Tasks;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace FiveSys.Presentation.Retaguarda.Web.Helpers
{
    public class TemPermissaoAttribute : TypeFilterAttribute
    {
        public TemPermissaoAttribute(FuncionalidadeEnum item, AcaoEnum action)
        : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { item, action };
        }
    }

    public class AuthorizeActionFilter : IAsyncActionFilter
    {
        private readonly FuncionalidadeEnum funcionalidade;
        private readonly AcaoEnum acao;

        public AuthorizeActionFilter(FuncionalidadeEnum funcionalidade, AcaoEnum acao)
        {
            this.funcionalidade = funcionalidade;
            this.acao = acao;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var autorizado = HtmlHelpers.VerificaPermissao(context.HttpContext, funcionalidade, acao);

            if (!autorizado)
            {
                if (context.HttpContext.Session.GetString("Usuario") == null)
                {
                    context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                                       {
                                       { "action", "Index" },
                                       { "controller", "Home" },
                                       { "area", "Acesso" }
                                       });
                }
                else
                {
                    context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                                       {
                                       { "action", "AcessoNegado" },
                                       { "controller", "Erro" },
                                       { "area", "Acesso" }
                                       });
                }
            }
            else
            {
                await next();
            }
        }
    }
}
