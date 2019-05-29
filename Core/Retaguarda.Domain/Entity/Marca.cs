using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Marca : EntityCrud<Marca>
    {
        public string Descricao { get; set; }

        public override AbstractValidator<Marca> Validador => new MarcaValidator();

        public override void PreencherDados(Marca data)
        {
            Descricao = data.Descricao;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
    }

    internal class MarcaValidator : AbstractValidator<Marca>
    {
        public MarcaValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull()
                .MaximumLength(100);
        }
    }
}
