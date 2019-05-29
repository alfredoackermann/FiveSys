using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Infrastructure.Test;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;



namespace Retaguarda.Application.Admin.Test
{
    [TestCaseOrderer("FileSys.Shared.Infrastructure.Test.OrdenarExecucao", "FileSys.Shared.Infrastructure.Test")]
    [Collection("Database collection")]
    public class TaraTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly ITaraAppService appService;

        public TaraTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.appService = fixture.Container.GetService<ITaraAppService>();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [Theory, OrdemExecucao(2)]
        [InlineData("Tara 1", "526")]
        [InlineData("Tara 2", "5")]
        [InlineData("Tara 3", "3")]
        [InlineData("Tara 4", "7")]
        public void InserirRegistroValido(string descricao, string peso)
        {
            var resultado = appService.Inserir(new TaraViewModel()
            {
                Descricao = descricao,
                Peso = peso

            });

            Assert.True(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);
        }

        [Fact, OrdemExecucao(10)]
        public void InserirRegistroDuplicado()
        {
            var parametros = appService.RecuperarTodos().Data;

            var resultado = appService.Inserir(new TaraViewModel()
            {
                Descricao = "Tara 2",
                Peso = "5"
            });

            Assert.False(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Erro_NomeDuplicado, resultado.Mensagem);
        }

        [Theory, OrdemExecucao(12)]
        [InlineData(null, null)]
        [InlineData("Descrição com mais de 100 caracteres para nao passar na validação 123423241234312451236535123541412342315123512342313251235412341234231512354 512 5123312 51235 3125132132 53 2135 1235 125 123  5 325123512 532315 21512 5232135 1325 3125132513251325531341234", "abc")]
        public void InserirRegistroInvalido(string descricao, string peso)
        {
            var resultado = appService.Inserir(new TaraViewModel()
            {
                Descricao = descricao,
                Peso = peso
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
        }

        [Theory, OrdemExecucao(28)]
        [InlineData("Tara teste 1", "1")]
        [InlineData("Tara teste 2", "2")]
        [InlineData("Tara teste 3", "3")]
        [InlineData("Tara teste 4", "4")]
        public void Atualizar(string descricao, string peso)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var tara = resultado.Data;

                Assert.NotNull(tara);

                tara.Descricao = descricao;
                tara.Peso = peso;

                var resultadoAtualizar = appService.Atualizar(tara);

                Assert.True(resultadoAtualizar.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Alteracao, resultadoAtualizar.Mensagem);

                resultado = appService.RecuperarPorId(tara.Id);

                Assert.True(resultado.Successo);

                var RamoAlterado = resultado.Data;

                Assert.NotNull(RamoAlterado);

                Assert.Equal(descricao, tara.Descricao);
                Assert.Equal(peso, tara.Peso);

                this.RecuperarTodos();
            }
        }

        [Theory, OrdemExecucao(31)]
        [InlineData(null, null)]
        [InlineData("Descrição com mais de 255 caracteres para nao passar na validação 123423241234312451236535123541412342315123512342313251235412341234231512354 512 5123312 51235 3125132132 53 2135 1235 125 123  5 325123512 532315 21512 5232135 1325 3125132513251325531341234", "abc")]
        public void AtualizarRegistroInvalido(string descricao, string peso)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var tara = resultado.Data;

                Assert.NotNull(tara);

                tara.Descricao = descricao;
                tara.Peso = peso;

                var resultadoAtualizar = appService.Atualizar(tara);

                Assert.False(resultadoAtualizar.Successo);
            }
        }

        [Fact, OrdemExecucao(40)]
        public void RemoverPorId()
        {
            var parametros = appService.RecuperarTodos().Data;

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
