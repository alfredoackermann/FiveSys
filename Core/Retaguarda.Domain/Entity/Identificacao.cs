using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Identificacao : EntityCrud<Identificacao>
    {
        public override AbstractValidator<Identificacao> Validador => new IdentificacaoValidator();

        public string MetadataId { get; set; }

        public TipoIdentificacao TipoIdentificacao { get; set; }
        public string TipoIdentificacaoId { get; set; }

        public string Descricao { get; set; }

        public bool Status { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(Identificacao data)
        {
            Descricao = data.Descricao;
            Status = data.Status;
            TipoIdentificacaoId = data.TipoIdentificacaoId;
        }
    }

    internal class IdentificacaoValidator : AbstractValidator<Identificacao>
    {
        public IdentificacaoValidator()
        {
            RuleFor(x => x.MetadataId)
                .NotNull();

            RuleFor(x => x.TipoIdentificacaoId)
                .NotNull();

            RuleFor(x => x.Descricao)
                .MaximumLength(100);

            RuleFor(x => x.TipoIdentificacaoId)
                .NotNull();
        }
    }
}
