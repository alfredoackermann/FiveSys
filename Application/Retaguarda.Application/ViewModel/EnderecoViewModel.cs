using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class EnderecoTelaViewModel
    {
        public EnderecoTelaViewModel()
        {

        }

        public EnderecoTelaViewModel(EnderecoViewModel endereco)
        {
            ApenasUmEndereco = true;
            Enderecos = new EnderecoViewModel[1] { endereco };
        }

        public EnderecoTelaViewModel(IEnumerable<EnderecoViewModel> enderecos)
        {
            ApenasUmEndereco = false;
            Enderecos = (enderecos.Count() > 0 ? enderecos.ToArray() : null);
        }

        public bool ApenasUmEndereco { get; set; }

        public EnderecoViewModel[] Enderecos { get; set; }

        #region Campos da tela

        public IDictionary<string, string> Ufs { get; set; }

        #endregion
    }

    public class EnderecoViewModel : BaseViewModel
    {
        public string Localizacao { get; set; }

        public int? Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UfCodigo { get; set; }
        public string UfNome { get; set; }

        public string Cep { get; set; }

        public string Complemento { get; set; }

        public int? CodigoMunicipio { get; set; }

        public int? Ibge { get; set; }

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<EnderecoViewModel, Endereco>()
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(orig => orig.UfCodigo.ToEnum<UfEnum>()))
                ;

            mapper.CreateMap<Endereco, EnderecoViewModel>()
                .ForMember(dest => dest.UfCodigo, opt => opt.MapFrom(orig => orig.Uf.Valor()))
                .ForMember(dest => dest.UfNome, opt => opt.MapFrom(orig => orig.Uf.Descricao()))
                ;
        }
    }
}
