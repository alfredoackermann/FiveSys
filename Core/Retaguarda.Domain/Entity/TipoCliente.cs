using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class TipoCliente : EntityCrud<TipoCliente>
    {
        public override AbstractValidator<TipoCliente> Validador => new TipoClienteValidator();

        public string Descricao { get; set; }

        public IEnumerable<Cliente> Clientes { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(TipoCliente data)
        {
            Descricao = data.Descricao;
        }
    }

    internal class TipoClienteValidator : AbstractValidator<TipoCliente>
    {
        public TipoClienteValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull()
                .MaximumLength(100);
        }
    }
}
