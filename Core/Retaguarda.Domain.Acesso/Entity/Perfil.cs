using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Entity
{
    public class Perfil : EntityCrud<Perfil>
    {
        public override AbstractValidator<Perfil> Validador => new PerfilValidator();

        public string Descricao { get; set; }

        public IEnumerable<Permissao> Permissoes { get; set; }

        public override void PreencherDados(Perfil data)
        {
            Descricao = data.Descricao;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
    }

    internal class PerfilValidator : AbstractValidator<Perfil>
    {
        public PerfilValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull()
                .MaximumLength(30);
        }
    }
}
