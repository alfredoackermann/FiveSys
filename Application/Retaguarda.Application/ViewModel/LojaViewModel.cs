using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using System.Collections.Generic;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class LojaViewModel : BaseViewModel
    {
        #region Propriedades
        public string Cadastro { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Responsavel { get; set; }
        public string RamoId { get; set; }
        public string Cnae { get; set; }
        public string SeriEnf { get; set; }
        public string SequenciaLnf { get; set; }
        public string Numero { get; set; }
        public string SerieCte { get; set; }
        public string SequencialCte { get; set; }
        public bool MicroempresaEstadual { get; set; }
        public bool ContribuinteIpi { get; set; }
        public bool OptanteSimples { get; set; }
        public bool SubstitutoTributario { get; set; }
        public bool OptanteSuperSimples { get; set; }
        public bool PermiteCreditoIcms { get; set; }
        public string TaxaIpi { get; set; }
        public string TaxaPis { get; set; }
        public string SimplesNacional { get; set; }
        public string Taxacofins { get; set; }
        public string CrtNome { get; set; }
        public string CrtCodigo { get; set; }
        public string Cnpj { get; set; }
        public string Titular { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string Fundacao { get; set; }
        public EnderecoTelaViewModel TelaEnderecos { get; set; } = new EnderecoTelaViewModel();

        public TelefoneTelaViewModel TelaTelefones { get; set; } = new TelefoneTelaViewModel();

        public EmailTelaViewModel TelaEmails { get; set; } = new EmailTelaViewModel();
        #endregion

        #region Campos da tela

        public IDictionary<string, string> Crts { get; set; }
        public IDictionary<string, string> Ramos { get; set; }
        public IDictionary<string, string> Ufs { get; set; }
        #endregion

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<LojaViewModel, Loja>()
                .ForMember(dest => dest.Telefones, opt => opt.MapFrom(orig => orig.TelaTelefones.Telefones))
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(orig => orig.TelaEnderecos.Enderecos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(orig => orig.TelaEmails.Emails))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(orig => orig.Cnpj.RemoverCaracteres()))
                .ForMember(dest => dest.Crt, opt => opt.MapFrom(orig => orig.CrtCodigo.ToEnum<CrtEnum>()))
                .ForMember(dest => dest.TaxaIpi, opt => opt.MapFrom(orig => orig.TaxaIpi))
                .ForMember(dest => dest.Taxacofins, opt => opt.MapFrom(orig => orig.Taxacofins))
                .ForMember(dest => dest.TaxaPis, opt => opt.MapFrom(orig => orig.TaxaPis))
                .ForMember(dest => dest.SimplesNacional, opt => opt.MapFrom(orig => orig.SimplesNacional))
                ;

            mapper.CreateMap<Loja, LojaViewModel>()
                .ForMember(dest => dest.TelaEnderecos, opt => opt.MapFrom(orig => new EnderecoTelaViewModel(Mapper.Map<IEnumerable<Endereco>, EnderecoViewModel[]>(orig.Enderecos))))
                .ForMember(dest => dest.TelaTelefones, opt => opt.MapFrom(orig => new TelefoneTelaViewModel(Mapper.Map<IEnumerable<Telefone>, TelefoneViewModel[]>(orig.Telefones))))
                .ForMember(dest => dest.TelaEmails, opt => opt.MapFrom(orig => new EmailTelaViewModel(Mapper.Map<IEnumerable<Email>, EmailViewModel[]>(orig.Emails))))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(orig => orig.Cnpj.FormatarCNPJ()))
                .ForMember(dest => dest.CrtCodigo, opt => opt.MapFrom(orig => orig.Crt.Valor()))
                .ForMember(dest => dest.CrtNome, opt => opt.MapFrom(orig => orig.Crt.Descricao()))
                .ForMember(dest => dest.Cadastro, opt => opt.MapFrom(orig => orig.Cadastro.DataFormatada()))
                .ForMember(dest => dest.Fundacao, opt => opt.MapFrom(orig => orig.Fundacao.DataFormatada()))
                .ForMember(dest => dest.TaxaIpi, opt => opt.MapFrom(orig => orig.TaxaIpi))
                .ForMember(dest => dest.Taxacofins, opt => opt.MapFrom(orig => orig.Taxacofins))
                .ForMember(dest => dest.TaxaPis, opt => opt.MapFrom(orig => orig.TaxaPis))
                .ForMember(dest => dest.SimplesNacional, opt => opt.MapFrom(orig => orig.SimplesNacional));

        }
    }
    public class LojaListaViewModel
    {
        public string Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Responsavel { get; set; }
        public string Ramo { get; set; }
        public string Cnpj { get; set; }

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<LojaDTO, LojaListaViewModel>()
                .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(orig => orig.RazaoSocial))
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(orig => orig.NomeFantasia))
                .ForMember(dest => dest.Responsavel, opt => opt.MapFrom(orig => orig.Responsavel))
                .ForMember(dest => dest.Ramo, opt => opt.MapFrom(orig => orig.Ramo))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(orig => orig.Cnpj))
               ;
        }
    }
}
