using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;
using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Tara : EntityCrud<Tara>
    {
        public override AbstractValidator<Tara> Validador => new TaraValidator();
        public string Descricao { get; set; }
        public decimal Peso { get; set; }

        public override void PreencherDados(Tara data)
        {
            Descricao = data.Descricao;
            Peso = data.Peso;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
    }

    internal class TaraValidator : AbstractValidator<Tara>
    {
        public TaraValidator()
        {
            RuleFor(x => x.Descricao)
            .MaximumLength(100)
            .NotNull();

            RuleFor(x => x.Peso)
                .NotNull();
        }
    }
}
