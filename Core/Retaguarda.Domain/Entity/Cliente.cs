using FileSys.Shared.Core.Domain.Impl.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public abstract class Cliente : EntityCrud<Cliente>
    {
        public override AbstractValidator<Cliente> Validador => new ClienteValidator();

        public string Nome { get; set; }

        public DateTime Cadastro { get; set; }

        public RegiaoEnum? Regiao { get; set; }

        public int TipoPessoa { get; set; }

        public string TipoClienteId { get; set; }
        public TipoCliente TipoCliente { get; set; }

        public string Referencias { get; set; }

        public bool CasaPropria { get; set; }

        public decimal? Renda { get; set; }

        public bool SituacaoCadastral { get; set; }

        public decimal? Limite { get; set; }

        public DateTime? UltimaCompra { get; set; }

        public decimal? Pontos { get; set; }

        public IEnumerable<Endereco> Enderecos { get; set; }

        public IEnumerable<Telefone> Telefones { get; set; }

        public IEnumerable<Identificacao> Identificacoes { get; set; }

        public IEnumerable<Email> Emails { get; set; }

        public override void PreencherDados(Cliente data)
        {
            Nome = data.Nome;
            Cadastro = data.Cadastro;
            Regiao = data.Regiao;
            TipoPessoa = data.TipoPessoa;
            TipoClienteId = data.TipoClienteId;
            Referencias = data.Referencias;
            CasaPropria = data.CasaPropria;
            Renda = data.Renda;
            SituacaoCadastral = data.SituacaoCadastral;
            Limite = data.Limite;
            UltimaCompra = data.UltimaCompra;
            Pontos = data.Pontos;
        }
    }

    public abstract class Cliente<TCliente> : Cliente where TCliente : Cliente
    {
        public abstract AbstractValidator<TCliente> Validador { get; }

        public virtual void PreencherDados(TCliente data)
        {
            base.PreencherDados(data);
        }
    }

    internal class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .MaximumLength(100);
        }
    }
}
