using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Migrations
{
    public partial class RecriaBancoDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Endereco = table.Column<string>(maxLength: 255, nullable: false),
                    MetadataId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Bairro = table.Column<string>(maxLength: 100, nullable: false),
                    Cep = table.Column<string>(maxLength: 9, nullable: true),
                    Cidade = table.Column<string>(maxLength: 100, nullable: false),
                    CodigoMunicipio = table.Column<int>(nullable: true),
                    Complemento = table.Column<string>(maxLength: 100, nullable: true),
                    Ibge = table.Column<int>(maxLength: 100, nullable: true),
                    Localizacao = table.Column<string>(maxLength: 100, nullable: false),
                    MetadataId = table.Column<string>(maxLength: 36, nullable: true),
                    Numero = table.Column<int>(nullable: true),
                    Uf = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Agencia = table.Column<int>(nullable: true),
                    AssistenciaTecnica = table.Column<bool>(nullable: false),
                    Banco = table.Column<int>(nullable: true),
                    Cadastro = table.Column<DateTime>(nullable: false),
                    Cnae = table.Column<string>(nullable: true),
                    Conta = table.Column<int>(nullable: true),
                    HomePage = table.Column<string>(maxLength: 255, nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(maxLength: 100, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Obs = table.Column<string>(nullable: true),
                    TipoPessoa = table.Column<int>(nullable: false),
                    Conjuge = table.Column<string>(maxLength: 255, nullable: true),
                    Cpf = table.Column<string>(maxLength: 14, nullable: true),
                    Emissao = table.Column<DateTime>(nullable: true),
                    Empresa = table.Column<string>(maxLength: 100, nullable: true),
                    EstadoCivil = table.Column<int>(nullable: true),
                    Filiacao = table.Column<string>(maxLength: 255, nullable: true),
                    Nascimento = table.Column<DateTime>(nullable: true),
                    OrgaoEmissor = table.Column<string>(maxLength: 3, nullable: true),
                    Profissao = table.Column<string>(maxLength: 100, nullable: true),
                    Rg = table.Column<string>(maxLength: 15, nullable: true),
                    Sexo = table.Column<int>(nullable: true),
                    UfOrgaoEmissor = table.Column<int>(nullable: true),
                    Cnpj = table.Column<string>(maxLength: 18, nullable: true),
                    Fundacao = table.Column<DateTime>(nullable: true),
                    InscricaoEstadual = table.Column<string>(maxLength: 20, nullable: true),
                    InscricaoMunicipal = table.Column<string>(maxLength: 20, nullable: true),
                    Titular = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ramos",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ramos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taras",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    Peso = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    MetadataId = table.Column<string>(maxLength: 36, nullable: true),
                    Numero = table.Column<string>(maxLength: 15, nullable: true),
                    Tipo = table.Column<int>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposCliente",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposIdentificacao",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIdentificacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposPreco",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPreco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposProduto",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProduto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Admissao = table.Column<DateTime>(nullable: true),
                    Comissao = table.Column<decimal>(nullable: true),
                    Cpf = table.Column<string>(maxLength: 11, nullable: true),
                    ExpirarSenha = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(maxLength: 20, nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    PerfilId = table.Column<string>(nullable: true),
                    Rg = table.Column<string>(maxLength: 15, nullable: true),
                    Senha = table.Column<string>(maxLength: 255, nullable: true),
                    SenhaTemporaria = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Cadastro = table.Column<DateTime>(nullable: true),
                    Cnae = table.Column<string>(maxLength: 30, nullable: true),
                    Cnpj = table.Column<string>(nullable: false),
                    ContribuinteIpi = table.Column<bool>(nullable: false),
                    Crt = table.Column<int>(nullable: false),
                    Fundacao = table.Column<DateTime>(nullable: true),
                    InscricaoEstadual = table.Column<string>(nullable: false),
                    InscricaoMunicipal = table.Column<string>(nullable: false),
                    MicroempresaEstadual = table.Column<bool>(nullable: false),
                    NomeFantasia = table.Column<string>(maxLength: 100, nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    OptanteSimples = table.Column<bool>(nullable: false),
                    OptanteSuperSimples = table.Column<bool>(nullable: false),
                    PermiteCreditoIcms = table.Column<bool>(nullable: false),
                    RamoId = table.Column<string>(maxLength: 36, nullable: false),
                    RazaoSocial = table.Column<string>(maxLength: 100, nullable: false),
                    Responsavel = table.Column<string>(maxLength: 100, nullable: true),
                    SequenciaLnf = table.Column<int>(nullable: false),
                    SequencialCte = table.Column<int>(nullable: false),
                    SeriEnf = table.Column<int>(nullable: false),
                    SerieCte = table.Column<int>(nullable: false),
                    SimplesNacional = table.Column<decimal>(nullable: true),
                    SubstitutoTributario = table.Column<bool>(nullable: false),
                    TaxaIpi = table.Column<decimal>(nullable: true),
                    TaxaPis = table.Column<decimal>(nullable: true),
                    Taxacofins = table.Column<decimal>(nullable: true),
                    Titular = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lojas_Ramos_RamoId",
                        column: x => x.RamoId,
                        principalTable: "Ramos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Cadastro = table.Column<DateTime>(nullable: false),
                    CasaPropria = table.Column<bool>(nullable: false),
                    Limite = table.Column<decimal>(nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Pontos = table.Column<decimal>(nullable: true),
                    Referencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regiao = table.Column<int>(nullable: true),
                    Renda = table.Column<decimal>(nullable: true),
                    SituacaoCadastral = table.Column<bool>(nullable: false),
                    TipoClienteId = table.Column<string>(nullable: true),
                    TipoPessoa = table.Column<int>(nullable: false),
                    UltimaCompra = table.Column<DateTime>(nullable: true),
                    Conjuge = table.Column<string>(maxLength: 255, nullable: true),
                    Cpf = table.Column<string>(maxLength: 14, nullable: true),
                    Emissao = table.Column<DateTime>(nullable: true),
                    Empresa = table.Column<string>(maxLength: 100, nullable: true),
                    EstadoCivil = table.Column<int>(nullable: true),
                    Filiacao = table.Column<string>(maxLength: 255, nullable: true),
                    Nascimento = table.Column<DateTime>(nullable: true),
                    OrgaoEmissor = table.Column<string>(maxLength: 3, nullable: true),
                    Profissao = table.Column<string>(maxLength: 100, nullable: true),
                    Rg = table.Column<string>(maxLength: 15, nullable: true),
                    Sexo = table.Column<int>(nullable: true),
                    UfOrgaoEmissor = table.Column<int>(nullable: true),
                    Cnpj = table.Column<string>(maxLength: 18, nullable: true),
                    Fundacao = table.Column<DateTime>(nullable: true),
                    InscricaoEstadual = table.Column<string>(maxLength: 20, nullable: true),
                    InscricaoMunicipal = table.Column<string>(maxLength: 20, nullable: true),
                    Titular = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_TiposCliente_TipoClienteId",
                        column: x => x.TipoClienteId,
                        principalTable: "TiposCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identificacoes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: true),
                    MetadataId = table.Column<string>(maxLength: 36, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TipoIdentificacaoId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identificacoes_TiposIdentificacao_TipoIdentificacaoId",
                        column: x => x.TipoIdentificacaoId,
                        principalTable: "TiposIdentificacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Id",
                table: "Clientes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoClienteId",
                table: "Clientes",
                column: "TipoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Id",
                table: "Emails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_Id",
                table: "Enderecos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_Id",
                table: "Fornecedores",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Identificacoes_Id",
                table: "Identificacoes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Identificacoes_TipoIdentificacaoId",
                table: "Identificacoes",
                column: "TipoIdentificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Lojas_Id",
                table: "Lojas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lojas_RamoId",
                table: "Lojas",
                column: "RamoId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_Id",
                table: "Marcas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parametros_Id",
                table: "Parametros",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ramos_Id",
                table: "Ramos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_Id",
                table: "Telefones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TiposCliente_Id",
                table: "TiposCliente",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TiposIdentificacao_Id",
                table: "TiposIdentificacao",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TiposPreco_Id",
                table: "TiposPreco",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TiposProduto_Id",
                table: "TiposProduto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_Id",
                table: "Unidades",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Id",
                table: "Usuarios",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Identificacoes");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Taras");

            migrationBuilder.DropTable(
                name: "Telefones");

            migrationBuilder.DropTable(
                name: "TiposPreco");

            migrationBuilder.DropTable(
                name: "TiposProduto");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TiposCliente");

            migrationBuilder.DropTable(
                name: "TiposIdentificacao");

            migrationBuilder.DropTable(
                name: "Ramos");
        }
    }
}
