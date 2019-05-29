using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Email : EntityCrud<Email>
    {
        public override AbstractValidator<Email> Validador => new EmailValidator();

        public string Endereco { get; set; }

        public string MetadataId { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(Email data)
        {
            Endereco = data.Endereco;
        }
    }

    internal class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.MetadataId)
                .NotNull();

            RuleFor(x => x.Endereco)
                .NotNull()
                .EmailAddress().WithMessage(Textos.Geral_Mensagem_Email_Invalido)
                .MaximumLength(255)
                ;
        }
    }
}
