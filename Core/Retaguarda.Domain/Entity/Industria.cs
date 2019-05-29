using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Industria : EntityCrud<Industria>
    {
        public string Descricao { get; set; }

        public string RamoId { get; set; }
        public Ramo Ramo { get; set; }

        public override AbstractValidator<Industria> Validador => new IndustriaValidator();

        public override void PreencherDados(Industria data)
        {
            Descricao = data.Descricao;
            RamoId = data.RamoId;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
        internal class IndustriaValidator : AbstractValidator<Industria>
        {
            public IndustriaValidator()
            {
                RuleFor(x => x.Descricao)
                    .NotNull()
                    .MaximumLength(100);
            }
        }
    }
}
