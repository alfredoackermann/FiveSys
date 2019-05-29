using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FluentValidation;
using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class PessoaJuridicaFornecedor : Fornecedor<PessoaJuridicaFornecedor>, IEntityCrud<PessoaJuridicaFornecedor>
    {
        public override AbstractValidator<PessoaJuridicaFornecedor> Validador => new PessoaJuridicaFornecedorValidator();

        public string Cnpj { get; set; }

        public string Titular { get; set; }

        public string InscricaoEstadual { get; set; }

        public string InscricaoMunicipal { get; set; }

        public DateTime? Fundacao { get; set; }

        public override void PreencherDados(PessoaJuridicaFornecedor data)
        {
            base.PreencherDados(data);

            Cnpj = data.Cnpj;
            Titular = data.Titular;
            InscricaoEstadual = data.InscricaoEstadual;
            InscricaoMunicipal = data.InscricaoMunicipal;
            Fundacao = data.Fundacao;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
    }

    internal class PessoaJuridicaFornecedorValidator : AbstractValidator<PessoaJuridicaFornecedor>
    {
        public PessoaJuridicaFornecedorValidator()
        {
            Include(new PessoaJuridicaFornecedorValidator());

            RuleFor(x => x.Cnpj)
                .MinimumLength(14)
                .MaximumLength(14);

            RuleFor(x => x.Titular)
                .MaximumLength(100);

            RuleFor(x => x.InscricaoEstadual)
                .MaximumLength(20);

            RuleFor(x => x.InscricaoMunicipal)
                .MaximumLength(20);//.When(x => x.TipoPessoa == Enum.PessoaTipoEnum.Juridica);
        }
    }
}
