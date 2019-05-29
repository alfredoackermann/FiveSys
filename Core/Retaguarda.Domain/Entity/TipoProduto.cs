using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class TipoProduto : EntityCrud<TipoProduto>
    {
        public override AbstractValidator<TipoProduto> Validador => new TipoProdutoValidator();

        public string Descricao { get; set; }
        public override void PreencherDados(TipoProduto data)
        {
            Descricao = data.Descricao;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
    }

    internal class TipoProdutoValidator : AbstractValidator<TipoProduto>
    {
        public TipoProdutoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull().WithMessage(Textos.Geral_Mensagem_Erro_Campo_null)
                .MaximumLength(100).WithMessage(Textos.Geral_Mensagem_Erro_Tamanho_Campo);
        }
    }
}
