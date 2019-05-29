using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class TipoIdentificacao : EntityCrud<TipoIdentificacao>
    {
        public override AbstractValidator<TipoIdentificacao> Validador => new TipoIdentificacaoValidator();

        public string Descricao { get; set; }

        public IEnumerable<Identificacao> Identificacoes { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(TipoIdentificacao data)
        {
            Descricao = data.Descricao;
        }
    }

    internal class TipoIdentificacaoValidator : AbstractValidator<TipoIdentificacao>
    {
        public TipoIdentificacaoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull()
                .MaximumLength(100);
        }
    }
}
