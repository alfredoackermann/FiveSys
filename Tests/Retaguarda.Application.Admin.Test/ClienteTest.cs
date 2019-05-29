using System.Globalization;
using System.Linq;
using System.Threading;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.CrossCutting.Tools;
using FileSys.Shared.Infrastructure.Test;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Retaguarda.Application.Admin.Test
{
    [TestCaseOrderer("FileSys.Shared.Infrastructure.Test.OrdenarExecucao", "FileSys.Shared.Infrastructure.Test")]
    [Collection("Database collection")]
    public class ClienteTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly IClienteAppService appService;

        public ClienteTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.appService = fixture.Container.GetService<IClienteAppService>();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [Fact, OrdemExecucao(10)]
        public void InserirPessoaFisicaApenasDados()
        {
            var regiao = RegiaoEnum.CentroOeste;
            var tipoPessoa = PessoaTipoEnum.Fisica;
            var uf = UfEnum.RioDeJaneiro;
            var estadoCivil = EstadoCivilEnum.Casado;
            var sexo = SexoEnum.Feminino;

            var novoCliente = new ClienteViewModel()
            {
                Nome = "Cliente 1",
                Cadastro = "10/01/2018",
                RegiaoCodigo = regiao.Valor(),
                TipoPessoaCodigo = tipoPessoa.Valor(),
                TipoClienteId = null,
                Referencias = "Referências do cliente 1",
                CasaPropria = true,
                Renda = "1000,00",
                SituacaoCadastral = false,
                Limite = "2000,00",
                UltimaCompra = "20/01/2018",
                Pontos = "38",
                // Dados pessoa fisica
                Cpf = "327.608.037-08",
                Rg = "11111111",
                OrgaoEmissor = "IFP",
                Emissao = "10/08/1978",
                UfOrgaoEmissorCodigo = uf.Valor(),
                Profissao = "Profissão do cliente 1",
                Empresa = "Empresa do cliente 1",
                Nascimento = "10/10/1945",
                Filiacao = "Pais do cliente 1",
                EstadoCivilCodigo = estadoCivil.Valor(),
                Conjuge = "Conjuge do cliente 1",
                SexoCodigo = sexo.Valor(),
            };

            var resultado = appService.Inserir(novoCliente);

            Assert.True(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);

            var clientes = appService.Listar().Data;

            Assert.Equal(1, clientes.Count);

            var cliente = clientes.Last();

            Assert.Equal(novoCliente.Cadastro, cliente.Cadastro);
            Assert.Equal(novoCliente.Limite, cliente.Limite);
            Assert.Equal(novoCliente.Nome, cliente.Nome);
            Assert.Equal(novoCliente.Pontos, cliente.Pontos);
            Assert.Equal(tipoPessoa.Valor(), cliente.TipoPessoa);
            Assert.Equal(regiao.Descricao(), cliente.Regiao);
            Assert.Equal(novoCliente.UltimaCompra, cliente.UltimaCompra);
        }

        [Fact, OrdemExecucao(11)]
        public void InserirPessoaJuridicaApenasDados()
        {
            var regiao = RegiaoEnum.CentroOeste;
            var tipoPessoa = PessoaTipoEnum.Juridica;

            var novoCliente = new ClienteViewModel()
            {
                Nome = "Cliente 1",
                Cadastro = "10/01/2018",
                RegiaoCodigo = regiao.Valor(),
                TipoPessoaCodigo = tipoPessoa.Valor(),
                TipoClienteId = null,
                Referencias = "Referências do cliente 1",
                CasaPropria = true,
                Renda = "1000,00",
                SituacaoCadastral = false,
                Limite = "2000,00",
                UltimaCompra = "20/01/2018",
                Pontos = "38",
                // Dados pessoa juridica
                Cnpj = "95.697.834/0001-04",
                Titular = "Titular do cliente 1",
                InscricaoEstadual = "22222222",
                InscricaoMunicipal = "9999999",
                Fundacao = "10/10/1945",
            };

            var resultado = appService.Inserir(novoCliente);

            Assert.True(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);

            var clientes = appService.Listar().Data;

            Assert.Equal(2, clientes.Count);

            var cliente = clientes.Last();

            Assert.Equal(novoCliente.Cadastro, cliente.Cadastro);
            Assert.Equal(novoCliente.Limite, cliente.Limite);
            Assert.Equal(novoCliente.Nome, cliente.Nome);
            Assert.Equal(novoCliente.Pontos, cliente.Pontos);
            Assert.Equal(tipoPessoa.Valor(), cliente.TipoPessoa);
            Assert.Equal(regiao.Descricao(), cliente.Regiao);
            Assert.Equal(novoCliente.UltimaCompra, cliente.UltimaCompra);
        }
    }
}
