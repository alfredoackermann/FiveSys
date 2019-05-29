using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Infrastructure.Test;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Retaguarda.Application.Admin.Test
{
    [TestCaseOrderer("FileSys.Shared.Infrastructure.Test.OrdenarExecucao", "FileSys.Shared.Infrastructure.Test")]
    [Collection("Database collection")]
    public class UnidadeTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly IUnidadeAppService appService;

        public UnidadeTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.appService = fixture.Container.GetService<IUnidadeAppService>();
        }

        [Theory, OrdemExecucao(10)]
        [InlineData(1, "KG")]
        [InlineData(2, "BG")]
        [InlineData(3, "GG")]
        [InlineData(4, "EE")]
        public void InserirRegistroValido(int contador, string descricao)
        {
            var resultado = appService.Inserir(new UnidadeViewModel()
            {
                Descricao = descricao
            });

            Assert.True(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);

            var parametros = appService.RecuperarTodos().Data;

            Assert.Equal(contador, parametros.Count);
        }

        [Fact, OrdemExecucao(11)]
        public void InserirRegistroDuplicado()
        {
            var parametros = appService.RecuperarTodos().Data;

            var resultado = appService.Inserir(new UnidadeViewModel()
            {
                Descricao = "GG"

            });

            Assert.False(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Erro_NomeDuplicado, resultado.Mensagem);
        }

        [Theory, OrdemExecucao(12)]
        [InlineData(null)]
        [InlineData("ACS")]
        public void InserirRegistroInvalido(string descricao)
        {
            var resultado = appService.Inserir(new UnidadeViewModel()
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
        [InlineData("A1")]
        [InlineData("A2")]
        [InlineData("A3")]
        [InlineData("A4")]
        public void Atualizar(string descricao)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var unidade = resultado.Data;

                Assert.NotNull(unidade);

                unidade.Descricao = descricao;

                var resultadoAtualizar = appService.Atualizar(unidade);

                Assert.True(resultadoAtualizar.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Alteracao, resultadoAtualizar.Mensagem);

                resultado = appService.RecuperarPorId(unidade.Id);

                Assert.True(resultado.Successo);

                var unidadeAlterado = resultado.Data;

                Assert.NotNull(unidadeAlterado);

                Assert.Equal(descricao, unidade.Descricao);

                this.RecuperarTodos();
            }
        }

        [Theory, OrdemExecucao(31)]
        [InlineData(null)]
        [InlineData("ABD")]
        public void AtualizarRegistroInvalido(string descricao)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var unidade = resultado.Data;

                Assert.NotNull(unidade);

                unidade.Descricao = descricao;

                var resultadoAtualizar = appService.Atualizar(unidade);

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
