using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.CrossCutting.Tools;
using FileSys.Shared.Infrastructure.Test;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;


namespace Retaguarda.Application.Admin.Test
{
    [TestCaseOrderer("FileSys.Shared.Infrastructure.Test.OrdenarExecucao", "FileSys.Shared.Infrastructure.Test")]
    [Collection("Database collection")]
    public class LojaTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly ILojaAppService appService;
        private readonly IRamoAppService ramoAppService;

        public LojaTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.appService = fixture.Container.GetService<ILojaAppService>();
            this.ramoAppService = fixture.Container.GetService<IRamoAppService>();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [Theory, OrdemExecucao(5)]
        [InlineData(1, "Ramo 1")]
        [InlineData(2, "Ramo 2")]
        [InlineData(3, "Ramo 3")]
        [InlineData(4, "Ramo 4")]

        public void InserirRamo(int contador, string descricao)
        {
            var resultado = ramoAppService.Inserir(new RamoViewModel()
            {
                Descricao = descricao
            });

            Assert.True(resultado.Successo);
            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);

            var parametros = ramoAppService.RecuperarTodos().Data;

            Assert.Equal(contador, parametros.Count);
        }

        [Fact, OrdemExecucao(10)]
        public void InserirLojaValida()
        {
            var Crt = CrtEnum.RegimeNormal;
            var uf = UfEnum.RioDeJaneiro;
            var TipoTelefoneResidencial = TipoTelefoneEnum.Residencial;
            var TipoTelefoneCelular = TipoTelefoneEnum.Celular;

            var Ramo = ramoAppService.RecuperarTodos().Data;

            Assert.NotEqual(0, Ramo.Count);

            var RamosId = Ramo.First().Id;

            Assert.NotNull(RamosId);

            var Endereco = new EnderecoViewModel()
            {
                Localizacao = "Endereço teste 1",
                Numero = 1,
                Bairro = "Bairro Teste 1",
                Cidade = "Cidade Teste 1",
                UfCodigo = uf.Valor(),
                UfNome = uf.Descricao(),
                Cep = "12345-123",
                Complemento = "Complemento Teste 1",
                CodigoMunicipio = 12345,
                Ibge = 12345
            };

            var Telefone1 = new TelefoneViewModel()
            {
                Numero = "1212345678",
                TipoCodigo = TipoTelefoneResidencial.Valor(),
                TipoNome = TipoTelefoneResidencial.Descricao()
            };

            var Telefone2 = new TelefoneViewModel()
            {
                Numero = "9898765432",
                TipoCodigo = TipoTelefoneCelular.Valor(),
                TipoNome = TipoTelefoneCelular.Descricao()
            };

            var Email1 = new EmailViewModel()
            {
                Endereco = "email1@email.com.br"
            };

            var Email2 = new EmailViewModel()
            {
                Endereco = "email2@email.com.br"
            };

            var novoLoja = new LojaViewModel()
            {
                Cadastro = "10/01/2018",
                RazaoSocial = "Razão Social da Loja",
                NomeFantasia = "Nome Fantasia da loja",
                Responsavel = "Responsavel pela Loja",
                RamoId = RamosId,
                Cnae = "12345",
                SeriEnf = "1",
                SequenciaLnf = "1",
                Numero = "1",
                SerieCte = "1",
                SequencialCte = "1",
                MicroempresaEstadual = false,
                ContribuinteIpi = false,
                OptanteSimples = false,
                SubstitutoTributario = false,
                OptanteSuperSimples = true,
                PermiteCreditoIcms = false,
                TaxaIpi = "10,00",
                TaxaPis = "20,00",
                SimplesNacional = "0",
                Taxacofins = "30,00",
                CrtNome = Crt.Descricao(),
                CrtCodigo = Crt.Valor(),
                Cnpj = "50483606000101",
                Titular = "Nome Titular",
                InscricaoEstadual = "12345",
                InscricaoMunicipal = "67890",
                Fundacao = "14/01/2015",
                TelaEnderecos = new EnderecoTelaViewModel()
                {
                    Enderecos = new EnderecoViewModel[] { Endereco }
                },
                TelaTelefones = new TelefoneTelaViewModel()
                {
                    Telefones = new TelefoneViewModel[] { Telefone1, Telefone2 }
                },
                TelaEmails = new EmailTelaViewModel()
                {
                    Emails = new EmailViewModel[] { Email1, Email2 }
                }
            };

            var resultado = appService.Inserir(novoLoja);

            Assert.True(resultado.Successo);

            Assert.Equal(Textos.Geral_Mensagem_Sucesso_Inclusao, resultado.Mensagem);
        }

        [Theory, OrdemExecucao(20)]
        [InlineData("Atualiza Razao Social", "Atualiza Nova Fantazia", "1234412", "1234132", "65014124000171")]
        public void AtualizarLojaValida(string RazaoSocial, string NomeFantasia, string InscricaoMunicipal, string InscricaoEstadual,
            string Cnpj)
        {
            var parametros = appService.RecuperarTodos().Data;

            var Crt = CrtEnum.SimplesNacionalExcessoSublimite;
            var uf = UfEnum.RioDeJaneiro;
            var TipoTelefoneResidencial = TipoTelefoneEnum.Residencial;

            Assert.NotEqual(0, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RecuperarPorId(parametro.Id);

                Assert.True(resultado.Successo);

                var loja = resultado.Data;

                Assert.NotNull(loja);

                loja.RazaoSocial = RazaoSocial;
                loja.NomeFantasia = NomeFantasia;
                loja.InscricaoMunicipal = InscricaoMunicipal;
                loja.InscricaoEstadual = InscricaoEstadual;
                loja.Cnpj = Cnpj;
                loja.CrtCodigo = Crt.Valor();
                loja.CrtNome = Crt.Descricao();

                var Endereco = new EnderecoViewModel()
                {
                    Localizacao = "Atualiza Endereço teste 1",
                    Numero = 1,
                    Bairro = "Atualiza Bairro Teste 1",
                    Cidade = "Atualiza Cidade Teste 1",
                    UfCodigo = uf.Valor(),
                    UfNome = uf.Descricao(),
                    Cep = "12345-123",
                    Complemento = "Atualiza Complemento Teste 1",
                    CodigoMunicipio = 12345,
                    Ibge = 12345
                };

                var Telefone1 = new TelefoneViewModel()
                {
                    Numero = "1212345678",
                    TipoCodigo = TipoTelefoneResidencial.Valor(),
                    TipoNome = TipoTelefoneResidencial.Descricao()
                };

                var Email1 = new EmailViewModel()
                {
                    Endereco = "Atualizaemail1@email.com.br"
                };

                loja.TelaEnderecos = new EnderecoTelaViewModel()
                {
                    Enderecos = new EnderecoViewModel[] { Endereco }
                };
                loja.TelaTelefones = new TelefoneTelaViewModel()
                {
                    Telefones = new TelefoneViewModel[] { Telefone1 }
                };
                loja.TelaEmails = new EmailTelaViewModel()
                {
                    Emails = new EmailViewModel[] { Email1 }
                };

                var resultadoAtualizar = appService.Atualizar(loja);

                Assert.True(resultadoAtualizar.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Alteracao, resultadoAtualizar.Mensagem);

                resultado = appService.RecuperarPorId(loja.Id);

                Assert.True(resultado.Successo);

                var lojaAlterado = resultado.Data;

                Assert.NotNull(lojaAlterado);

                Assert.Equal(RazaoSocial, loja.RazaoSocial);
                Assert.Equal(NomeFantasia, loja.NomeFantasia);
                Assert.Equal(InscricaoMunicipal, loja.InscricaoMunicipal);
                Assert.Equal(InscricaoEstadual, loja.InscricaoEstadual);
                Assert.Equal(Cnpj, loja.Cnpj);
                Assert.Equal(Crt.Valor(), loja.CrtCodigo);
                Assert.Equal(Crt.Descricao(), loja.CrtNome);

                this.RecuperarTodos();
            }
        }

        [Fact, OrdemExecucao(25)]
        public void ListarTodos()
        {
            var resultado = appService.Listar();

            Assert.True(resultado.Successo);

            Assert.Equal(1, resultado.Data.Count);
        }

        [Fact, OrdemExecucao(30)]
        public void RecuperarTodos()
        {
            var resultado = appService.RecuperarTodos();

            Assert.True(resultado.Successo);

            Assert.Equal(1, resultado.Data.Count);
        }

        [Fact, OrdemExecucao(35)]
        public void RecuperarDropDown()
        {
            var resultado = appService.RecuperarDropdown();

            Assert.True(resultado.Successo);

            Assert.Equal(1, resultado.Data.Count);
        }

        [Fact, OrdemExecucao(40)]
        public void InserirLojaInvalida()
        {
            var uf = UfEnum.RioDeJaneiro;

            var Endereco = new EnderecoViewModel()
            {
                Localizacao = "Endereço teste 1",
                Numero = 1,
                Bairro = "Bairro Teste 1",
                Cidade = "Cidade Teste 1",
                UfCodigo = uf.Valor(),
                UfNome = uf.Descricao(),
                Cep = "12345-123",
                Complemento = "Complemento Teste 1",
                CodigoMunicipio = 12345,
                Ibge = 12345
            };
            var novoLoja = new LojaViewModel()
            {
                Cadastro = "10/01/2018",
                RazaoSocial = "teste razao social",
                NomeFantasia = "",
                Responsavel = "Responsavel pela Loja",
                RamoId = "",
                Cnae = "12345",
                SeriEnf = "1",
                SequenciaLnf = "1",
                Numero = "1",
                SerieCte = "1",
                SequencialCte = "1",
                MicroempresaEstadual = false,
                ContribuinteIpi = false,
                OptanteSimples = false,
                SubstitutoTributario = false,
                OptanteSuperSimples = true,
                PermiteCreditoIcms = false,
                TaxaIpi = "10,00",
                TaxaPis = "20,00",
                SimplesNacional = "0",
                Taxacofins = "30,00",
                CrtNome = "",
                CrtCodigo = "",
                Cnpj = "",
                Titular = "Nome Titular",
                InscricaoEstadual = "",
                InscricaoMunicipal = "",
                Fundacao = "14/01/2015",
                TelaEnderecos = new EnderecoTelaViewModel()
                {
                    Enderecos = new EnderecoViewModel[] { Endereco }
                },
                TelaTelefones = new TelefoneTelaViewModel()
                {
                    Telefones = new TelefoneViewModel[] { }
                },
                TelaEmails = new EmailTelaViewModel()
                {
                    Emails = new EmailViewModel[] { }
                }
            };

            var resultado = appService.Inserir(novoLoja);

            Assert.False(resultado.Successo);
        }

        [Fact, OrdemExecucao(50)]
        public void RemoverPorId()
        {
            var parametros = appService.RecuperarTodos().Data;

            Assert.Equal(1, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = appService.RemoverPorId(parametro.Id);

                Assert.True(resultado.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Exclusao, resultado.Mensagem);
            }

            parametros = appService.RecuperarTodos().Data;

            Assert.Equal(0, parametros.Count);
        }

        [Fact, OrdemExecucao(55)]
        public void RemoverRamoPorId()
        {
            var parametros = ramoAppService.RecuperarTodos().Data;

            Assert.Equal(4, parametros.Count);

            foreach (var parametro in parametros)
            {
                var resultado = ramoAppService.RemoverPorId(parametro.Id);

                Assert.True(resultado.Successo);
                Assert.Equal(Textos.Geral_Mensagem_Sucesso_Exclusao, resultado.Mensagem);
            }

            parametros = ramoAppService.RecuperarTodos().Data;

            Assert.Equal(0, parametros.Count);
        }
    }
}


