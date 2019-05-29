using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FluentValidation;
using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class PessoaFisicaFornecedor : Fornecedor<PessoaFisicaFornecedor>, IEntityCrud<PessoaFisicaFornecedor>
    {
        public override AbstractValidator<PessoaFisicaFornecedor> Validador => new PessoaFisicaFornecedorValidator();

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string OrgaoEmissor { get; set; }

        public DateTime? Emissao { get; set; }

        public UfEnum? UfOrgaoEmissor { get; set; }

        public string Profissao { get; set; }

        public string Empresa { get; set; }

        public DateTime? Nascimento { get; set; }

        public string Filiacao { get; set; }

        public EstadoCivilEnum? EstadoCivil { get; set; }

        public string Conjuge { get; set; }

        public SexoEnum? Sexo { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(PessoaFisicaFornecedor data)
        {
            base.PreencherDados(data);

            Cpf = data.Cpf;
            Rg = data.Rg;
            OrgaoEmissor = data.OrgaoEmissor;
            Emissao = data.Emissao;
            UfOrgaoEmissor = data.UfOrgaoEmissor;
            Profissao = data.Profissao;
            Empresa = data.Empresa;
            Nascimento = data.Nascimento;
            Filiacao = data.Filiacao;
            EstadoCivil = data.EstadoCivil;
            Conjuge = data.Conjuge;
            Sexo = data.Sexo;
        }
    }

    internal class PessoaFisicaFornecedorValidator : AbstractValidator<PessoaFisicaFornecedor>
    {
        public PessoaFisicaFornecedorValidator()
        {
            Include(new PessoaFisicaFornecedorValidator());

            RuleFor(x => x.Cpf)
                .CPFValido()
                .MinimumLength(11)
                .MaximumLength(11);

            RuleFor(x => x.Rg)
                .MaximumLength(15);

            RuleFor(x => x.OrgaoEmissor)
                .MinimumLength(3)
                .MaximumLength(3);

            RuleFor(x => x.Profissao)
                .MaximumLength(255);

            RuleFor(x => x.Empresa)
                .MaximumLength(255);

            RuleFor(x => x.Filiacao)
                .MaximumLength(255);

            RuleFor(x => x.Conjuge)
                .MaximumLength(255);
        }
    }
}
