using System;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Usuario : EntityCrud<Usuario>
    {
        public override AbstractValidator<Usuario> Validador => new UsuarioValidator();

        public DateTime? Admissao { get; set; }

        public string Login { get; set; }

        public string Nome { get; set; }

        public string Rg { get; set; }

        public string Cpf { get; set; }

        public DateTime? Nascimento { get; set; }

        public decimal? Comissao { get; set; }

        public string Observacao { get; set; }

        public bool ExpirarSenha { get; set; }

        public StatusEnum? Status { get; set; }

        public Email Email { get; set; }

        public string PerfilId { get; set; }

        public string Senha { get; set; }

        public bool SenhaTemporaria { get; set; }


        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(Usuario data)
        {
            Status = data.Status;
            Admissao = data.Admissao;
            Nome = data.Nome;
            Rg = data.Rg;
            Cpf = data.Cpf;
            Nascimento = data.Nascimento;
            Comissao = data.Comissao;
            Observacao = data.Observacao;
            ExpirarSenha = data.ExpirarSenha;
            PerfilId = data.PerfilId;
        }
    }

    internal class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nome)
                .MaximumLength(100)
                .NotNull();

            RuleFor(x => x.PerfilId)
                .NotNull();

            RuleFor(x => x.Cpf)
                .MinimumLength(11)
                .MaximumLength(11);

            RuleFor(x => x.Rg)
                .MaximumLength(15);
        }
    }
}
