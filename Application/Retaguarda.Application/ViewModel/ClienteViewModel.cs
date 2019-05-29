using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using System.Collections.Generic;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class ClienteViewModel : BaseViewModel
    {
        public string Nome { get; set; }

        public string Cadastro { get; set; }

        public string RegiaoCodigo { get; set; }

        public string TipoPessoaCodigo { get; set; }

        public string TipoClienteId { get; set; }

        public string Referencias { get; set; }

        public bool CasaPropria { get; set; }

        public string Renda { get; set; }

        public bool SituacaoCadastral { get; set; }

        public string Limite { get; set; }

        public string UltimaCompra { get; set; }

        public string Pontos { get; set; }

        public EnderecoTelaViewModel TelaEnderecos { get; set; } = new EnderecoTelaViewModel();

        public TelefoneTelaViewModel TelaTelefones { get; set; } = new TelefoneTelaViewModel();

        public EmailTelaViewModel TelaEmails { get; set; } = new EmailTelaViewModel();

        public IdentificacaoTelaViewModel TelaIdentificacoes { get; set; } = new IdentificacaoTelaViewModel();

        #region Pessoa Fisica

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string OrgaoEmissor { get; set; }

        public string Emissao { get; set; }

        public string UfOrgaoEmissorCodigo { get; set; }

        public string Profissao { get; set; }

        public string Empresa { get; set; }

        public string Nascimento { get; set; }

        public string Filiacao { get; set; }

        public string EstadoCivilCodigo { get; set; }

        public string Conjuge { get; set; }

        public string SexoCodigo { get; set; }

        #endregion

        #region Pessoa Juridica

        public string Cnpj { get; set; }

        public string Titular { get; set; }

        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }

        public string Fundacao { get; set; }

        #endregion

        #region Campos da tela

        public IDictionary<string, string> Regioes { get; set; }
        public IDictionary<string, string> TiposPessoa { get; set; }
        public IDictionary<string, string> TiposCliente { get; set; }
        public IDictionary<string, string> Ufs { get; set; }
        public IDictionary<string, string> EstadosCivis { get; set; }
        public IDictionary<string, string> Sexos { get; set; }

        #endregion

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<ClienteViewModel, Cliente>()
                .Include<ClienteViewModel, PessoaFisica>()
                .Include<ClienteViewModel, PessoaJuridica>()
                .ForMember(dest => dest.TipoPessoa, opt => opt.MapFrom(orig => orig.TipoPessoaCodigo.ToEnum<PessoaTipoEnum>()))
                .ForMember(dest => dest.Regiao, opt => opt.MapFrom(orig => orig.RegiaoCodigo.ToEnum<RegiaoEnum>()))
                .ForMember(dest => dest.Telefones, opt => opt.MapFrom(orig => orig.TelaTelefones.Telefones))
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(orig => orig.TelaEnderecos.Enderecos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(orig => orig.TelaEmails.Emails))
                .ForMember(dest => dest.Identificacoes, opt => opt.MapFrom(orig => orig.TelaIdentificacoes.Identificacoes))
               ;

            mapper.CreateMap<ClienteViewModel, PessoaFisica>()
                .ForMember(dest => dest.UfOrgaoEmissor, opt => opt.MapFrom(orig => orig.UfOrgaoEmissorCodigo.ToEnum<RegiaoEnum>()))
                .ForMember(dest => dest.EstadoCivil, opt => opt.MapFrom(orig => orig.EstadoCivilCodigo.ToEnum<EstadoCivilEnum>()))
                .ForMember(dest => dest.Sexo, opt => opt.MapFrom(orig => orig.SexoCodigo.ToEnum<SexoEnum>()))
                ;

            mapper.CreateMap<ClienteViewModel, PessoaJuridica>()
                ;

            mapper.CreateMap<Cliente, ClienteViewModel>()
                .Include<PessoaFisica, ClienteViewModel>()
                .Include<PessoaJuridica, ClienteViewModel>()
                .ForMember(dest => dest.TipoPessoaCodigo, opt => opt.MapFrom(orig => orig.TipoPessoa))
                .ForMember(dest => dest.RegiaoCodigo, opt => opt.MapFrom(orig => orig.Regiao.Valor()))
                .ForMember(dest => dest.TelaEnderecos, opt => opt.MapFrom(orig => new EnderecoTelaViewModel(Mapper.Map<IEnumerable<Endereco>, EnderecoViewModel[]>(orig.Enderecos))))
                .ForMember(dest => dest.TelaTelefones, opt => opt.MapFrom(orig => new TelefoneTelaViewModel(Mapper.Map<IEnumerable<Telefone>, TelefoneViewModel[]>(orig.Telefones))))
                .ForMember(dest => dest.TelaEmails, opt => opt.MapFrom(orig => new EmailTelaViewModel(Mapper.Map<IEnumerable<Email>, EmailViewModel[]>(orig.Emails))))
                .ForMember(dest => dest.TelaIdentificacoes, opt => opt.MapFrom(orig => new IdentificacaoTelaViewModel(Mapper.Map<IEnumerable<Identificacao>, IdentificacaoViewModel[]>(orig.Identificacoes))))
                .ForMember(dest => dest.Cadastro, opt => opt.MapFrom(orig => orig.Cadastro.DataFormatada()))
                .ForMember(dest => dest.UltimaCompra, opt => opt.MapFrom(orig => orig.UltimaCompra.DataFormatada()))
                ;

            mapper.CreateMap<PessoaFisica, ClienteViewModel>()
                .ForMember(dest => dest.UfOrgaoEmissorCodigo, opt => opt.MapFrom(orig => orig.UfOrgaoEmissor.Valor()))
                .ForMember(dest => dest.EstadoCivilCodigo, opt => opt.MapFrom(orig => orig.EstadoCivil.Valor()))
                .ForMember(dest => dest.SexoCodigo, opt => opt.MapFrom(orig => orig.Sexo.Valor()))
                .ForMember(dest => dest.Emissao, opt => opt.MapFrom(orig => orig.Emissao.DataFormatada()))
                .ForMember(dest => dest.Nascimento, opt => opt.MapFrom(orig => orig.Nascimento.DataFormatada()))
                ;

            mapper.CreateMap<PessoaJuridica, ClienteViewModel>()
                .ForMember(dest => dest.Fundacao, opt => opt.MapFrom(orig => orig.Fundacao.DataFormatada()))
                ;
        }
    }

    public class ClienteListaViewModel
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Cadastro { get; set; }

        public string Regiao { get; set; }

        public string TipoCliente { get; set; }

        public string Limite { get; set; }

        public string UltimaCompra { get; set; }

        public string Pontos { get; set; }

        public string TipoPessoa { get; set; }

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<ClienteDTO, ClienteListaViewModel>()
                .ForMember(dest => dest.Cadastro, opt => opt.MapFrom(orig => orig.Cadastro.DataFormatada()))
                .ForMember(dest => dest.UltimaCompra, opt => opt.MapFrom(orig => orig.UltimaCompra.DataFormatada()))
                .ForMember(dest => dest.Regiao, opt => opt.MapFrom(orig => orig.Regiao.Descricao()))
                .ForMember(dest => dest.TipoCliente, opt => opt.MapFrom(orig => orig.TipoCliente))
               ;
        }
    }
}
