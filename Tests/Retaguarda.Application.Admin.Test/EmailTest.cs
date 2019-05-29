using System;
using System.Linq;
using FileSys.Shared.Infrastructure.Test;
using FiveSys.Core.Retaguarda.Domain.Admin.Entity;
using FiveSys.Core.Retaguarda.Domain.Admin.Interface.Service;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Retaguarda.Application.Admin.Test
{
    [TestCaseOrderer("FileSys.Shared.Infrastructure.Test.OrdenarExecucao", "FileSys.Shared.Infrastructure.Test")]
    [Collection("Database collection")]
    public class EmailTest
    {
        private readonly InicializacaoFixture fixture;
        private readonly IEmailService service;

        public EmailTest(InicializacaoFixture fixture)
        {
            this.fixture = fixture;
            this.service = fixture.Container.GetService<IEmailService>();
        }

        [Theory, OrdemExecucao(10)]
        [InlineData(1, null, "email1@email.com.br")]
        [InlineData(2, "email1@email.com.br")]
        [InlineData(3, "email2@email.com.br")]
        public void InserirRegistroValido(int contador, string id, string endereco)
        {
            var metadataId = "METADATAID_1";

            var emailModel = new Email()
            {
                Id = id,
                Endereco = endereco,
            };

            var resultado = service.Processar(emailModel, null, metadataId);

            Assert.True(resultado.IsValid);

            var emails = service.RecuperarTodos(metadataId);

            Assert.Equal(contador, emails.Count());
        }

        [Fact, OrdemExecucao(11)]
        public void InserirRegistrosValidos()
        {
            var emailsModel = new Email[2] {
                new Email()
                {
                    Endereco = "email3@email.com.br",
                },
                new Email()
                {
                    Endereco = "email4@email.com.br",
                }
            };

            var resultado = service.Processar(emailsModel, null, "METADATAID_2");

            Assert.True(resultado.IsValid);
        }

        [Fact, OrdemExecucao(12)]
        public void InserirRegistroInvalido()
        {
            var emailModel = new Email()
            {
                Endereco = "emailinvalido@email.com.br",
            };

            var resultado = service.Processar(emailModel, null, "METADATAID_3");

            Assert.True(resultado.IsValid);
        }

        [Fact, OrdemExecucao(20)]
        public void RecuperarTodos()
        {
            Assert.Throws<NotSupportedException>(() => service.RecuperarTodos());
        }

        [Fact, OrdemExecucao(21)]
        public void RecuperarPorId()
        {
            var id = "EMAIL_2";

            Assert.Throws<NotSupportedException>(() => service.RecuperarPorId(id));
        }

        [Fact, OrdemExecucao(22)]
        public void RecuperarDropdown()
        {
            Assert.Throws<NotSupportedException>(() => service.Pesquisar(x => x.Id == "EMAIL_2"));
        }

        [Fact, OrdemExecucao(23)]
        public void RecuperarPorMetadata()
        {
            var resultado = service.RecuperarTodos("METADATAID_1");

            Assert.Equal(3, resultado.Count());
        }

        [Fact, OrdemExecucao(30)]
        public void AtualizarRegistro()
        {
            var metadataId = "METADATAID_1";
            var emailId = "EMAIL_2";
            var novoEmail = "novoemail@email.com.br";

            var emails = service.RecuperarTodos(metadataId);

            // Verifica se retorna 3 registros
            Assert.Equal(3, emails.Count());

            var email = emails.SingleOrDefault(x => x.Id == emailId);

            var emailModel = new Email()
            {
                Id = emailId,
                Endereco = novoEmail,
            };

            var resultado = service.Processar(emailModel, email, metadataId);

            var emailsAlterados = service.RecuperarTodos(metadataId);

            // Verifica se continua retornando 3 registros
            Assert.Equal(3, emailsAlterados.Count());

            var emailAlterado = emailsAlterados.SingleOrDefault(x => x.Id == emailId);

            // Verifica se alterou o endereco
            Assert.Equal(novoEmail, emailAlterado.Endereco);
            // Verifica se o metadataId nao foi modificado
            Assert.Equal(metadataId, emailAlterado.MetadataId);
            // Verifica se o id nao foi alterado
            Assert.Equal(emailId, emailAlterado.Id);
        }

        [Fact, OrdemExecucao(31)]
        public void AtualizarRegistros()
        {
            var metadataId = "METADATAID_1";

            var emails = service.RecuperarTodos(metadataId);

            // Verifica se retorna 3 registros
            Assert.Equal(3, emails.Count());

            var emailsModel = new Email[2] {
                new Email()
                {
                    Id = "EMAIL_1",
                    Endereco = "novoemail1@email.com.br",
                },
                new Email()
                {
                    Id = "EMAIL_2",
                    Endereco = "novoemail2@email.com.br",
                }
            };

            var resultado = service.Processar(emailsModel, emails, metadataId);

            var emailsAlterados = service.RecuperarTodos(metadataId);

            // Verifica se continua retornando 3 registros
            Assert.Equal(3, emailsAlterados.Count());

            // Nao precisa testar se os valores continuam o mesmo porque foi testado em AtualizarRegistro()
        }

        [Fact, OrdemExecucao(40)]
        public void RemoverPorId()
        {
            service.RemoverPorId("EMAIL_1");

            var emailsExistentes = service.RecuperarTodos("METADATAID_1");

            Assert.Equal(2, emailsExistentes.Count());
        }

        [Fact, OrdemExecucao(41)]
        public void RemoverPorMetadataId()
        {
            service.RemoverTodos("METADATAID_1");

            var emailsExistentes = service.RecuperarTodos("METADATAID_1");

            Assert.Empty(emailsExistentes);
        }
    }
}
