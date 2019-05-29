using System;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class PessoaJuridica : Cliente<PessoaJuridica>, IEntityCrud<PessoaJuridica>
    {
        public override AbstractValidator<PessoaJuridica> Validador => new PessoaJuridicaValidator();

        public string Cnpj { get; set; }

        public string Titular { get; set; }

        public string InscricaoEstadual { get; set; }

        public string InscricaoMunicipal { get; set; }

        public DateTime? Fundacao { get; set; }

        public override void PreencherDados(PessoaJuridica data)
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

    internal class PessoaJuridicaValidator : AbstractValidator<PessoaJuridica>
    {
        public PessoaJuridicaValidator()
        {
            Include(new ClienteValidator());

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
