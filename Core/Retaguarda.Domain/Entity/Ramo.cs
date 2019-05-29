using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Ramo : EntityCrud<Ramo>
    {
        public override AbstractValidator<Ramo> Validador => new RamoValidator();

        public string Descricao { get; set; }

        public IEnumerable<Industria> Industrias { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(Ramo data)
        {
            Descricao = data.Descricao;
        }
    }

    internal class RamoValidator : AbstractValidator<Ramo>
    {
        public RamoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull()
                .MaximumLength(100);
        }
    }
}
