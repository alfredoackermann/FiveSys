using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Entity
{
    public class Permissao: EntityCrud<Permissao>
    {
        public override AbstractValidator<Permissao> Validador => new PermissaoValidator();

        public string PerfilId { get; set; }
        public Perfil Perfil { get; set; }

        public FuncionalidadeEnum Funcionalidade { get; set; }

        public int Acoes { get; set; }

        public override void PreencherDados(Permissao data)
        {
            PerfilId = data.PerfilId;
            Funcionalidade = data.Funcionalidade;
            Acoes = data.Acoes;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
    }

    internal class PermissaoValidator : AbstractValidator<Permissao>
    {
        public PermissaoValidator()
        {
            RuleFor(x => x.PerfilId)
                .NotNull();
        }
    }
}
