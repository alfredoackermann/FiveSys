using System.Linq;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Infrastructure.Test;
using Microsoft.Extensions.DependencyInjection;
using Xunit;



namespace Retaguarda.Application.Admin.Test
{
    [TestCaseOrderer("FileSys.Shared.Infrastructure.Test.OrdenarExecucao", "FileSys.Shared.Infrastructure.Test")]
    [Collection("Database collection")]
    public class TipoClienteTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly ITipoClienteAppService appService;

        public TipoClienteTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.appService = fixture.Container.GetService<ITipoClienteAppService>();
        }

        [Theory, OrdemExecucao(10)]
        [InlineData(1, "Tipo Cliente 1")]
        [InlineData(2, "Tipo Cliente 2")]
        [InlineData(3, "Tipo Cliente 3")]
        [InlineData(4, "Tipo Cliente 4")]

        public void InserirRegistroValido(int contador, string descricao)
        {
            var resultado = appService.Inserir(new TipoClienteViewModel()
            {
                Descricao = descricao
            });

            Assert.True(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);

            var parametros = appService.RecuperarTodos().Data;

            Assert.Equal(contador, parametros.Count);

            this.appService.RecuperarTodos();
        }

        [Fact, OrdemExecucao(11)]
        public void InserirRegistroDuplicado()
        {
            var resultado = appService.Inserir(new TipoClienteViewModel()
            {
                Descricao = "Tipo Cliente 3"

            });

            Assert.False(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Erro_NomeDuplicado, resultado.Mensagem);
        }

        [Theory, OrdemExecucao(12)]
        [InlineData(null)]
        [InlineData("Descrição com mais de 255 caracteres para nao passar na validação 123423241234312451236535123541412342315123512342313251235412341234231512354 512 5123312 51235 3125132132 53 2135 1235 125 123  5 325123512 532315 21512 5232135 1325 3125132513251325531341234")]
        public void InserirRegistroInvalido(string descricao)
        {
            var resultado = appService.Inserir(new TipoClienteViewModel()
            {
                Descricao = descricao
            });

            Assert.False(resultado.Successo);
            Assert.True(resultado.Mensagem.Count() > 0);

            var parametros = appService.RecuperarTodos().Data;
        }

        [Fact, OrdemExecucao(20)]
        public void RecuperarTodos()
        {
            var resultado = appService.RecuperarTodos();

            Assert.True(resultado.Successo);

            Assert.Equal(4, resultado.Data.Count);
        }

        [Fact, OrdemExecucao(21)]
        public void RecuperarPorId()
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            var id = parametros.First().Id;

            Assert.NotNull(id);

            var resultado = appService.RecuperarPorId(id);

            Assert.True(resultado.Successo);

            var parametro = resultado.Data;

            Assert.NotNull(parametro);

            Assert.Equal(id, parametro.Id);
        }

        [Fact, OrdemExecucao(22)]
        public void RecuperarDropdown()
        {
            var resultado = appService.RecuperarDropdown();

            Assert.True(resultado.Successo);
            Assert.Equal(4, resultado.Data.Count);
        }

        [Theory, OrdemExecucao(30)]
        [InlineData("Tipo Cliente teste 1")]
        [InlineData("Tipo Cliente teste 2")]
        [InlineData("Tipo Cliente teste 3")]
        [InlineData("Tipo Cliente teste 4")]
        public void Atualizar(string descricao)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var tipoCliente = resultado.Data;

                Assert.NotNull(tipoCliente);

                tipoCliente.Descricao = descricao;

                var resultadoAtualizar = appService.Atualizar(tipoCliente);

                Assert.True(resultadoAtualizar.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Alteracao, resultadoAtualizar.Mensagem);

                resultado = appService.RecuperarPorId(tipoCliente.Id);

                Assert.True(resultado.Successo);

                var tipoClienteAlterado = resultado.Data;

                Assert.NotNull(tipoClienteAlterado);

                Assert.Equal(descricao, tipoCliente.Descricao);

                this.RecuperarTodos();
            }
        }

        [Theory, OrdemExecucao(31)]
        [InlineData(null)]
        [InlineData("Descrição com mais de 255 caracteres para nao passar na validação 123423241234312451236535123541412342315123512342313251235412341234231512354 512 5123312 51235 3125132132 53 2135 1235 125 123  5 325123512 532315 21512 5232135 1325 3125132513251325531341234")]
        public void AtualizarRegistroInvalido(string descricao)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var tipoCliente = resultado.Data;

                Assert.NotNull(tipoCliente);

                tipoCliente.Descricao = descricao;

                var resultadoAtualizar = appService.Atualizar(tipoCliente);

                Assert.False(resultadoAtualizar.Successo);
            }
        }

        [Fact, OrdemExecucao(40)]
        public void RemoverPorId()
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.Equal(4, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RemoverPorId(parametro.Id);

                Assert.True(resultado.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Exclusao, resultado.Mensagem);
            }

            parametros = appService.RecuperarTodos().Data;

            Assert.Equal(0, parametros.Count);
        }
    }
}
