using System.Linq;
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
    public class ParametroTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly IParametroAppService appService;

        public ParametroTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.appService = fixture.Container.GetService<IParametroAppService>();
        }

        [Theory, OrdemExecucao(10)]
        [InlineData(1, null, "ParametroTexto", ParametroTipoEnum.Texto, "Valor 1", "Descrição do parâmetro de teste do tipo texto")]
        [InlineData(2, "PARAMETRO_1", "ParametroNumero", ParametroTipoEnum.Numero, "2000", "Descrição do parâmetro de teste do tipo número")]
        [InlineData(3, "PARAMETRO_2", "ParametroData", ParametroTipoEnum.Data, "13/01/2018", "Descrição do parâmetro de teste do tipo data")]
        [InlineData(4, "PARAMETRO_3", "ParametroVariante", ParametroTipoEnum.Texto, "Texto inicial", "Descrição do parâmetro de teste do tipo que muda de texto para número")]
        public void InserirRegistroValido(int contador, string id, string nome, ParametroTipoEnum tipo, string valor, string descricao)
        {
            var novoParametro = new ParametroViewModel()
            {
                Id = id,
                Descricao = descricao,
                Nome = nome,
                TipoCodigo = tipo.Valor(),
                Valor = valor
            };

            var resultado = appService.Inserir(novoParametro);

            Assert.True(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);

            var parametros = appService.RecuperarTodos().Data;

            Assert.Equal(contador, parametros.Count);

            var ultimoParametro = parametros.Last();

            Assert.Equal(nome, ultimoParametro.Nome);
            Assert.Equal(tipo.Valor(), ultimoParametro.TipoCodigo);
            Assert.Equal(valor, ultimoParametro.Valor);
            Assert.Equal(descricao, ultimoParametro.Descricao);
        }

        [Fact, OrdemExecucao(11)]
        public void InserirRegistroDuplicado()
        {
            var parametros = appService.RecuperarTodos().Data;

            var resultado = appService.Inserir(new ParametroViewModel()
            {
                Descricao = "Qualuqer descrição",
                Nome = "ParametroTexto",
                TipoCodigo = ParametroTipoEnum.Texto.Valor(),
                Valor = "Qualuqer valor"
            });

            Assert.False(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Erro_NomeDuplicado, resultado.Mensagem);
        }

        [Theory, OrdemExecucao(12)]
        [InlineData(null, ParametroTipoEnum.Texto, null, null)]
        [InlineData("Nome com mais de 50 caracteres para nao passar na validação", ParametroTipoEnum.Texto, "2000", null)]
        [InlineData(null, ParametroTipoEnum.Texto, "2000", "Descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres")]
        [InlineData(null, ParametroTipoEnum.Texto, "Valor com mais de 20 caracteres", null)]
        [InlineData("ParametroDataInvalida", ParametroTipoEnum.Data, "Texto", null)]
        [InlineData("ParametroNumeroInvalido", ParametroTipoEnum.Numero, "Texto", null)]
        public void InserirRegistroInvalido(string nome, ParametroTipoEnum tipo, string valor, string descricao)
        {
            var resultado = appService.Inserir(new ParametroViewModel()
            {
                Descricao = descricao,
                Nome = nome,
                TipoCodigo = tipo.Valor(),
                Valor = valor
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
            var id = "PARAMETRO_1";

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

        [Theory, OrdemExecucao(23)]
        [InlineData("ParametroTexto")]
        [InlineData("ParametroNumero")]
        [InlineData("ParametroData")]
        public void RecuperarPorNome(string nome)
        {
            var resultado = appService.RecuperarPorNome(nome);

            Assert.True(resultado.Successo);
            Assert.Equal(nome, resultado.Data.Nome);
        }

        [Fact, OrdemExecucao(24)]
        public void RecuperarPorNomeErro()
        {
            var resultado = appService.RecuperarPorNome(null);

            Assert.False(resultado.Successo);
        }

        [Theory, OrdemExecucao(30)]
        [InlineData("ParametroTexto", ParametroTipoEnum.Texto, "Valor 1 alterado", "Descrição do parâmetro de teste do tipo texto com valor alterado")]
        [InlineData("ParametroNumero", ParametroTipoEnum.Numero, "10000", "Descrição do parâmetro de teste do tipo número com valor alterado")]
        [InlineData("ParametroData", ParametroTipoEnum.Data, "14/01/2017", "Descrição do parâmetro de teste do tipo data com valor alterado")]
        [InlineData("ParametroVariante", ParametroTipoEnum.Numero, "1000", "Descrição do parâmetro de teste do tipo que mudou de texto para número")]
        public void Atualizar(string nome, ParametroTipoEnum tipo, string valor, string descricao)
        {
            var resultado = appService.RecuperarPorNome(nome);

            Assert.True(resultado.Successo);

            var parametro = resultado.Data;

            Assert.NotNull(parametro);

            parametro.Descricao = descricao;
            parametro.Nome = nome;
            parametro.TipoCodigo = tipo.Valor();
            parametro.Valor = valor;

            var resultadoAtualizar = appService.Atualizar(parametro);

            Assert.True(resultadoAtualizar.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Alteracao, resultadoAtualizar.Mensagem);

            resultado = appService.RecuperarPorNome(nome);

            Assert.True(resultado.Successo);

            var parametroAlterado = resultado.Data;

            Assert.NotNull(parametroAlterado);

            Assert.Equal(descricao, parametro.Descricao);
            Assert.Equal(nome, parametro.Nome);
            Assert.Equal(tipo.Valor(), parametro.TipoCodigo);
            Assert.Equal(valor, parametro.Valor);

            this.RecuperarTodos();
        }

        [Theory, OrdemExecucao(31)]
        [InlineData("ParametroTexto", ParametroTipoEnum.Texto, null, null)]
        [InlineData("ParametroTexto", ParametroTipoEnum.Texto, "SóComValor", null)]
        [InlineData("ParametroTexto", ParametroTipoEnum.Texto, null, "SóComDescricao")]
        [InlineData("ParametroTexto", ParametroTipoEnum.Texto, "2000", "Descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres, descrição repetida com mais de 255 caracteres")]
        [InlineData("ParametroTexto", ParametroTipoEnum.Texto, "Valor com mais de 20 caracteres", null)]
        [InlineData("ParametroTexto", ParametroTipoEnum.Data, "Texto", null)]
        [InlineData("ParametroTexto", ParametroTipoEnum.Numero, "Texto", null)]
        public void AtualizarRegistroInvalido(string nome, ParametroTipoEnum tipo, string valor, string descricao)
        {
            var resultado = appService.RecuperarPorNome(nome);

            Assert.True(resultado.Successo);

            var parametro = resultado.Data;

            Assert.NotNull(parametro);

            parametro.Descricao = descricao;
            parametro.Nome = nome;
            parametro.TipoCodigo = tipo.Valor();
            parametro.Valor = valor;

            var resultadoAtualizar = appService.Atualizar(parametro);

            Assert.False(resultadoAtualizar.Successo);
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
