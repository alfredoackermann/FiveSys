using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class TipoPreco : EntityCrud<TipoPreco>
    {
        public override AbstractValidator<TipoPreco> Validador => new TipoPrecoValidator();

        public string Descricao { get; set; }

        public override void PreencherDados(TipoPreco data)
        {
            Descricao = data.Descricao;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
    }
    internal class TipoPrecoValidator : AbstractValidator<TipoPreco>
    {
        public TipoPrecoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull().WithMessage(Textos.Geral_Mensagem_Erro_Campo_null)
                .MaximumLength(100).WithMessage(Textos.Geral_Mensagem_Erro_Tamanho_Campo);
        }
    }
}
