using FileSys.Retaguarda.Application.Acesso.Impl;
using FileSys.Retaguarda.Application.Acesso.Interface;
using FileSys.Retaguarda.Application.Admin.Impl;
using FileSys.Retaguarda.Application.Admin.Interface;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Acesso.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Service;
using FiveSys.Retaguarda.Infrastructure.Data.Acesso;
using FiveSys.Retaguarda.Infrastructure.Data.Admin;
using FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FiveSys.Retaguarda.Infrastructure.CrossCutting.IoC
{
    public class Configuracao
    {
        public static void InjecaoDependencia(IServiceCollection services)
        {
            // Deixar em ordem alfabetica de entidade para ficar mais facil de procurar
            #region Diversos
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailRepository, EmailRepository>();

            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<ITelefoneService, TelefoneService>();
            services.AddScoped<ITelefoneRepository, TelefoneRepository>();
            #endregion

            #region Admin
            services.AddScoped<IParametroAppService, ParametroAppService>();
            services.AddScoped<IParametroService, ParametroService>();
            services.AddScoped<IParametroRepository, ParametroRepository>();

            services.AddScoped<IRamoAppService, RamoAppService>();
            services.AddScoped<IRamoService, RamoService>();
            services.AddScoped<IRamoRepository, RamoRepository>();

            services.AddScoped<IIndustriaAppService, IndustriaAppService>();
            services.AddScoped<IIndustriaService, IndustriaService>();
            services.AddScoped<IIndustriaRepository, IndustriaRepository>();

            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion

            #region Cliente
            services.AddScoped<IClienteAppService, ClienteAppService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddScoped<IIdentificacaoService, IdentificacaoService>();
            services.AddScoped<IIdentificacaoRepository, IdentificacaoRepository>();

            services.AddScoped<IPessoaFisicaService, PessoaFisicaService>();
            services.AddScoped<IPessoaFisicaRepository, PessoaFisicaRepository>();

            services.AddScoped<IPessoaJuridicaService, PessoaJuridicaService>();
            services.AddScoped<IPessoaJuridicaRepository, PessoaJuridicaRepository>();

            services.AddScoped<ITipoClienteAppService, TipoClienteAppService>();
            services.AddScoped<ITipoClienteService, TipoClienteService>();
            services.AddScoped<ITipoClienteRepository, TipoClienteRepository>();

            services.AddScoped<ITipoIdentificacaoAppService, TipoIdentificacaoAppService>();
            services.AddScoped<ITipoIdentificacaoService, TipoIdentificacaoService>();
            services.AddScoped<ITipoIdentificacaoRepository, TipoIdentificacaoRepository>();
            #endregion

            #region Loja
            services.AddScoped<ILojaAppService, LojaAppService>();
            services.AddScoped<ILojaService, LojaService>();
            services.AddScoped<ILojaRepository, LojaRepository>();
            #endregion

            #region Produto
            services.AddScoped<IFornecedorAppService, FornecedorAppService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            services.AddScoped<IPessoaFisicaFornecedorService, PessoaFisicaFornecedorService>();
            services.AddScoped<IPessoaFisicaFornecedorRepository, PessoaFisicaFornecedorRepository>();

            services.AddScoped<IPessoaJuridicaFornecedorService, PessoaJuridicaFornecedorService>();
            services.AddScoped<IPessoaJuridicaFornecedorRepository, PessoaJuridicaFornecedorRepository>();

            services.AddScoped<IMarcaAppService, MarcaAppService>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();

            services.AddScoped<ITaraAppService, TaraAppService>();
            services.AddScoped<ITaraService, TaraService>();
            services.AddScoped<ITaraRepository, TaraRepository>();

            services.AddScoped<ITipoPrecoAppService, TipoPrecoAppService>();
            services.AddScoped<ITipoPrecoService, TipoPrecoService>();
            services.AddScoped<ITipoPrecoRepository, TipoPrecoRepository>();

            services.AddScoped<ITipoProdutoAppService, TipoProdutoAppService>();
            services.AddScoped<ITipoProdutoService, TipoProdutoService>();
            services.AddScoped<ITipoProdutoRepository, TipoProdutoRepository>();

            services.AddScoped<IUnidadeAppService, UnidadeAppService>();
            services.AddScoped<IUnidadeService, UnidadeService>();
            services.AddScoped<IUnidadeRepository, UnidadeRepository>();
            #endregion

            #region Acesso

            services.AddScoped<IPerfilAppService, PerfilAppService>();
            services.AddScoped<IPerfilService, PerfilService>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();

            services.AddScoped<IPermissaoService, PermissaoService>();
            services.AddScoped<IPermissaoRepository, PermissaoRepository>();

            services.AddScoped<RetaguardaAcessoContext>();

            #endregion

            services.AddScoped<RetaguardaAdminContext>();
        }
    }
}
