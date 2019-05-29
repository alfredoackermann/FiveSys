using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using System.Collections.Generic;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class FornecedorViewModel : BaseViewModel
    {
        #region Propriedades
        public string Numero { get; set; }
        public string TipoPessoaCodigo { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Cadastro { get; set; }
        public string Cnae { get; set; }
        public string HomePage { get; set; }
        public bool AssistenciaTecnica { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Obs { get; set; }
        public EnderecoTelaViewModel TelaEnderecos { get; set; } = new EnderecoTelaViewModel();
        public TelefoneTelaViewModel TelaTelefones { get; set; } = new TelefoneTelaViewModel();
        public EmailTelaViewModel TelaEmails { get; set; } = new EmailTelaViewModel();
        #endregion

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
        public IDictionary<string, string> TiposPessoa { get; set; }
        public IDictionary<string, string> Ufs { get; set; }
        public IDictionary<string, string> EstadosCivis { get; set; }
        public IDictionary<string, string> Sexos { get; set; }
        #endregion

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<FornecedorViewModel, Fornecedor>()
                .Include<FornecedorViewModel, PessoaFisicaFornecedor>()
                .Include<FornecedorViewModel, PessoaJuridicaFornecedor>()
                .ForMember(dest => dest.Telefones, opt => opt.MapFrom(orig => orig.TelaTelefones.Telefones))
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(orig => orig.TelaEnderecos.Enderecos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(orig => orig.TelaEmails.Emails))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(orig => orig.Numero))
                .ForMember(dest => dest.TipoPessoa, opt => opt.MapFrom(orig => orig.TipoPessoaCodigo.ToEnum<PessoaTipoEnum>()))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(orig => orig.Nome))
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(orig => orig.NomeFantasia))
                .ForMember(dest => dest.Cadastro, opt => opt.MapFrom(orig => orig.Cadastro))
                .ForMember(dest => dest.Cnae, opt => opt.MapFrom(orig => orig.Cnae))
                .ForMember(dest => dest.HomePage, opt => opt.MapFrom(orig => orig.HomePage))
                .ForMember(dest => dest.AssistenciaTecnica, opt => opt.MapFrom(orig => orig.AssistenciaTecnica))
                .ForMember(dest => dest.Banco, opt => opt.MapFrom(orig => orig.Banco))
                .ForMember(dest => dest.Agencia, opt => opt.MapFrom(orig => orig.Agencia))
                .ForMember(dest => dest.Conta, opt => opt.MapFrom(orig => orig.Conta))
                .ForMember(dest => dest.Obs, opt => opt.MapFrom(orig => orig.Obs));

            mapper.CreateMap<Fornecedor, FornecedorViewModel>()
                .Include<PessoaFisicaFornecedor, FornecedorViewModel>()
                .Include<PessoaJuridicaFornecedor, FornecedorViewModel>()
                .ForMember(dest => dest.TelaEnderecos, opt => opt.MapFrom(orig => new EnderecoTelaViewModel(Mapper.Map<IEnumerable<Endereco>, EnderecoViewModel[]>(orig.Enderecos))))
                .ForMember(dest => dest.TelaTelefones, opt => opt.MapFrom(orig => new TelefoneTelaViewModel(Mapper.Map<IEnumerable<Telefone>, TelefoneViewModel[]>(orig.Telefones))))
                .ForMember(dest => dest.TelaEmails, opt => opt.MapFrom(orig => new EmailTelaViewModel(Mapper.Map<IEnumerable<Email>, EmailViewModel[]>(orig.Emails))))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(orig => orig.Numero))
                .ForMember(dest => dest.TipoPessoaCodigo, opt => opt.MapFrom(orig => orig.TipoPessoa))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(orig => orig.Nome))
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(orig => orig.NomeFantasia))
                .ForMember(dest => dest.Cadastro, opt => opt.MapFrom(orig => orig.Cadastro.DataFormatada()))
                .ForMember(dest => dest.Cnae, opt => opt.MapFrom(orig => orig.Cnae))
                .ForMember(dest => dest.HomePage, opt => opt.MapFrom(orig => orig.HomePage))
                .ForMember(dest => dest.AssistenciaTecnica, opt => opt.MapFrom(orig => orig.AssistenciaTecnica))
                .ForMember(dest => dest.Banco, opt => opt.MapFrom(orig => orig.Banco))
                .ForMember(dest => dest.Agencia, opt => opt.MapFrom(orig => orig.Agencia))
                .ForMember(dest => dest.Conta, opt => opt.MapFrom(orig => orig.Conta))
                .ForMember(dest => dest.Obs, opt => opt.MapFrom(orig => orig.Obs));

            mapper.CreateMap<FornecedorViewModel, PessoaFisicaFornecedor>()
                .ForMember(dest => dest.UfOrgaoEmissor, opt => opt.MapFrom(orig => orig.UfOrgaoEmissorCodigo.ToEnum<RegiaoEnum>()))
                .ForMember(dest => dest.EstadoCivil, opt => opt.MapFrom(orig => orig.EstadoCivilCodigo.ToEnum<EstadoCivilEnum>()))
                .ForMember(dest => dest.Sexo, opt => opt.MapFrom(orig => orig.SexoCodigo.ToEnum<SexoEnum>()))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(orig => orig.Cpf.RemoverCaracteres()))
                ;

            mapper.CreateMap<PessoaFisicaFornecedor, FornecedorViewModel>()
                .ForMember(dest => dest.UfOrgaoEmissorCodigo, opt => opt.MapFrom(orig => orig.UfOrgaoEmissor.Valor()))
                .ForMember(dest => dest.EstadoCivilCodigo, opt => opt.MapFrom(orig => orig.EstadoCivil.Valor()))
                .ForMember(dest => dest.SexoCodigo, opt => opt.MapFrom(orig => orig.Sexo.Valor()))
                .ForMember(dest => dest.Emissao, opt => opt.MapFrom(orig => orig.Emissao.DataFormatada()))
                .ForMember(dest => dest.Nascimento, opt => opt.MapFrom(orig => orig.Nascimento.DataFormatada()))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(orig => orig.Cpf.FormataCPF()))
                ;

            mapper.CreateMap<FornecedorViewModel, PessoaJuridicaFornecedor>()
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(orig => orig.Cnpj.RemoverCaracteres()))
                ;

            mapper.CreateMap<PessoaJuridicaFornecedor, FornecedorViewModel>()
                .ForMember(dest => dest.Fundacao, opt => opt.MapFrom(orig => orig.Fundacao.DataFormatada()))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(orig => orig.Cnpj.FormatarCNPJ()))
                ;
        }
    }

    public class FornecedorListaViewModel
    {
        public string Id { get; set; }

        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Cadastro { get; set; }
        public string Numero { get; set; }


        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<FornecedorDTO, FornecedorListaViewModel>()
                .ForMember(dest => dest.Cadastro, opt => opt.MapFrom(orig => orig.Cadastro.DataFormatada()))
               ;
        }
    }
}
