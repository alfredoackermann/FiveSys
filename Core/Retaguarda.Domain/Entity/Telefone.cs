using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Telefone : EntityCrud<Telefone>
    {
        public override AbstractValidator<Telefone> Validador => new TelefoneValidator();

        public TipoTelefoneEnum? Tipo { get; set; }

        public string Numero { get; set; }

        public string MetadataId { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(Telefone data)
        {
            Tipo = data.Tipo;
            Numero = data.Numero;
        }
    }

    internal class TelefoneValidator : AbstractValidator<Telefone>
    {
        public TelefoneValidator()
        {
            RuleFor(x => x.MetadataId)
                .NotNull();

            RuleFor(x => x.Tipo);

            RuleFor(x => x.Numero)
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(15);
        }
    }
}
