﻿using FileSys.Retaguarda.Application.Admin.Interface;
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
    public class TipoProdutoTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly ITipoProdutoAppService appService;

        public TipoProdutoTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.appService = fixture.Container.GetService<ITipoProdutoAppService>();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [Theory, OrdemExecucao(1)]
        [InlineData(1, "Tipo de Produto 1")]
        [InlineData(2, "Tipo de Produto 2")]
        [InlineData(3, "Tipo de Produto 3")]
        [InlineData(4, "Tipo de Produto 4")]
        public void InserirRegistroValido(int contador, string descricao)
        {
            var resultado = appService.Inserir(new TipoProdutoViewModel()
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

            var resultado = appService.Inserir(new TipoProdutoViewModel()
            {
                Descricao = "Tipo de Produto 2"

            });

            Assert.False(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Erro_NomeDuplicado, resultado.Mensagem);
        }

        [Theory, OrdemExecucao(12)]
        [InlineData(null)]
        [InlineData("Descrição com mais de 100 caracteres para nao passar na validação 123423241234312451236535123541412342315123512342313251235412341234231512354 512 5123312 51235 3125132132 53 2135 1235 125 123  5 325123512 532315 21512 5232135 1325 3125132513251325531341234")]
        public void InserirRegistroInvalido(string descricao)
        {
            var resultado = appService.Inserir(new TipoProdutoViewModel()
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

        [Theory, OrdemExecucao(28)]
        [InlineData("Tipo Produto teste 1")]
        [InlineData("Tipo Produto teste 2")]
        [InlineData("Tipo Produto teste 3")]
        [InlineData("Tipo Produto teste 4")]
        public void Atualizar(string descricao)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var tipoProduto = resultado.Data;

                Assert.NotNull(tipoProduto);

                tipoProduto.Descricao = descricao;

                var resultadoAtualizar = appService.Atualizar(tipoProduto);

                Assert.True(resultadoAtualizar.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Alteracao, resultadoAtualizar.Mensagem);

                resultado = appService.RecuperarPorId(tipoProduto.Id);

                Assert.True(resultado.Successo);

                var tipoProdutoAlterado = resultado.Data;

                Assert.NotNull(tipoProdutoAlterado);

                Assert.Equal(descricao, tipoProduto.Descricao);

                this.RecuperarTodos();
            }
        }

        [Theory, OrdemExecucao(31)]
        [InlineData(null)]
        [InlineData("Descrição com mais de 100 caracteres para nao passar na validação 123423241234312451236535123541412342315123512342313251235412341234231512354 512 5123312 51235 3125132132 53 2135 1235 125 123  5 325123512 532315 21512 5232135 1325 3125132513251325531341234")]
        public void AtualizarRegistroInvalido(string descricao)
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var tipoProduto = resultado.Data;

                Assert.NotNull(tipoProduto);

                tipoProduto.Descricao = descricao;

                var resultadoAtualizar = appService.Atualizar(tipoProduto);

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
