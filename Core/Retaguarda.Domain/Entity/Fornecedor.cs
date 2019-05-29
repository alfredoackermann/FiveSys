using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public abstract class Fornecedor : EntityCrud<Fornecedor>
    {
        public override AbstractValidator<Fornecedor> Validador => new FornecedorValidator();

        public int Numero { get; set; }
        public int TipoPessoa { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime Cadastro { get; set; }
        public string Cnae { get; set; }
        public string HomePage { get; set; }
        public bool AssistenciaTecnica { get; set; }
        public int? Banco { get; set; }
        public int? Agencia { get; set; }
        public int? Conta { get; set; }
        public string Obs { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }
        public IEnumerable<Telefone> Telefones { get; set; }
        public IEnumerable<Email> Emails { get; set; }

        public override void PreencherDados(Fornecedor data)
        {
            Numero = data.Numero;
            TipoPessoa = data.TipoPessoa;
            Nome = data.Nome;
            NomeFantasia = data.NomeFantasia;
            Cadastro = data.Cadastro;
            Cnae = data.Cnae;
            HomePage = data.HomePage;
            AssistenciaTecnica = data.AssistenciaTecnica;
            Banco = data.Banco;
            Agencia = data.Agencia;
            Conta = data.Conta;
            Obs = data.Obs;
        }

        //public override ResultadoValidacao Validar()
        //{
        //    return ExecutarValidacaoPadrao(this);
        //}
    }
    public abstract class Fornecedor<TFornecedor> : Fornecedor where TFornecedor : Fornecedor
    {
        public abstract AbstractValidator<TFornecedor> Validador { get; }

        public virtual void PreencherDados(TFornecedor data)
        {
            base.PreencherDados(data);
        }
    }
    internal class FornecedorValidator : AbstractValidator<Fornecedor>
    {
        public FornecedorValidator()
        {
            RuleFor(x => x.Numero)
                .NotNull().WithMessage(Textos.Geral_Mensagem_Erro_Campo_null);

            RuleFor(x => x.TipoPessoa);
            RuleFor(x => x.Nome)
                .NotNull().WithMessage(Textos.Geral_Mensagem_Erro_Campo_null)
                .MaximumLength(100).WithMessage(Textos.Geral_Mensagem_Erro_Tamanho_Campo);

            RuleFor(x => x.NomeFantasia)
                .NotNull().WithMessage(Textos.Geral_Mensagem_Erro_Campo_null)
                .MaximumLength(100).WithMessage(Textos.Geral_Mensagem_Erro_Tamanho_Campo);

            RuleFor(x => x.Cadastro)
                .NotNull().WithMessage(Textos.Geral_Mensagem_Erro_Campo_null);

            RuleFor(x => x.Cnae);
            RuleFor(x => x.HomePage);
            RuleFor(x => x.AssistenciaTecnica);
            RuleFor(x => x.Banco);
            RuleFor(x => x.Agencia);
            RuleFor(x => x.Conta);
            RuleFor(x => x.Obs);
        }
    }
}
