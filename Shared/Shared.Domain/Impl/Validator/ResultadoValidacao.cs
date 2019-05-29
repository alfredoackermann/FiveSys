using System.Collections.Generic;
using FluentValidation.Results;

namespace FileSys.Shared.Core.Domain.Impl.Validator
{
    public class ResultadoValidacao : ValidationResult
    {
        public ResultadoValidacao() : base() { }

        public ResultadoValidacao(IEnumerable<ValidationFailure> erros) : base(erros) { }

        public override bool IsValid => base.Errors.Count == 0;
    }
}
