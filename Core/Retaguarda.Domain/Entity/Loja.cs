using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Loja : EntityCrud<Loja>
    {
        public override AbstractValidator<Loja> Validador => new LojaValidator();

        #region Propriedades 

        public DateTime? Cadastro { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Responsavel { get; set; }
        public Ramo Ramo { get; set; }
        public string RamoId { get; set; }
        public string Cnae { get; set; }
        public int SeriEnf { get; set; }
        public int SequenciaLnf { get; set; }
        public int Numero { get; set; }
        public int SerieCte { get; set; }
        public int SequencialCte { get; set; }
        public bool MicroempresaEstadual { get; set; }
        public bool ContribuinteIpi { get; set; }
        public bool OptanteSimples { get; set; }
        public bool SubstitutoTributario { get; set; }
        public bool OptanteSuperSimples { get; set; }
        public bool PermiteCreditoIcms { get; set; }
        public decimal? TaxaIpi { get; set; }
        public decimal? TaxaPis { get; set; }
        public decimal? SimplesNacional { get; set; }
        public decimal? Taxacofins { get; set; }
        public CrtEnum? Crt { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }
        public IEnumerable<Telefone> Telefones { get; set; }
        public IEnumerable<Email> Emails { get; set; }
        public string Cnpj { get; set; }
        public string Titular { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public DateTime? Fundacao { get; set; }
        #endregion

        #region Preencher dados
        public override void PreencherDados(Loja loja)
        {
            Cadastro = loja.Cadastro;
            RazaoSocial = loja.RazaoSocial;
            NomeFantasia = loja.NomeFantasia;
            Responsavel = loja.Responsavel;
            RamoId = loja.RamoId;
            Cnae = loja.Cnae;
            Cnpj = loja.Cnpj;
            Titular = loja.Titular;
            InscricaoEstadual = loja.InscricaoEstadual;
            InscricaoMunicipal = loja.InscricaoMunicipal;
            Fundacao = loja.Fundacao;
            SeriEnf = loja.SeriEnf;
            SequenciaLnf = loja.SequenciaLnf;
            Numero = loja.Numero;
            SerieCte = loja.SerieCte;
            SequencialCte = loja.SequencialCte;
            Crt = loja.Crt;
            MicroempresaEstadual = loja.MicroempresaEstadual;
            ContribuinteIpi = loja.ContribuinteIpi;
            OptanteSimples = loja.OptanteSimples;
            SubstitutoTributario = loja.SubstitutoTributario;
            OptanteSuperSimples = loja.OptanteSuperSimples;
            PermiteCreditoIcms = loja.PermiteCreditoIcms;
            TaxaIpi = loja.TaxaIpi;
            TaxaPis = loja.TaxaPis;
            SimplesNacional = loja.SimplesNacional;
            Taxacofins = loja.Taxacofins;
        }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }
        #endregion        
    }

    internal class LojaValidator : AbstractValidator<Loja>
    {
        public LojaValidator()
        {
            RuleFor(x => x.RazaoSocial)
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.NomeFantasia)
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.RamoId)
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Cnpj)
                .NotNull()
                .CNPJValido()
                .MinimumLength(14)
                .MaximumLength(14);

            RuleFor(x => x.Titular)
                .MaximumLength(100);

            RuleFor(x => x.InscricaoEstadual)
                .NotNull()
                .MaximumLength(20);

            RuleFor(x => x.InscricaoMunicipal)
                .NotNull()
                .MaximumLength(20);

            RuleFor(x => x.Crt)
                .NotNull();
        }
    }
}
