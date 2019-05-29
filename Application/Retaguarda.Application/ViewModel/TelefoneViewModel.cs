using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using System.Collections.Generic;
using System.Linq;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class TelefoneTelaViewModel
    {
        public TelefoneTelaViewModel()
        {

        }

        public TelefoneTelaViewModel(TelefoneViewModel telefone)
        {
            ApenasUmTelefone = true;
            Telefones = new TelefoneViewModel[1] { telefone };
        }

        public TelefoneTelaViewModel(IEnumerable<TelefoneViewModel> telefones)
        {
            ApenasUmTelefone = false;
            Telefones = (telefones.Count() > 0 ? telefones.ToArray() : null);
        }

        public bool ApenasUmTelefone { get; set; }

        public TelefoneViewModel[] Telefones { get; set; }

        #region Campos da tela

        public IDictionary<string, string> TiposTelefone { get; set; }

        #endregion
    }

    public class TelefoneViewModel : BaseViewModel
    {
        public string TipoCodigo { get; set; }

        public string TipoNome { get; set; }

        public string Numero { get; set; }

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<TelefoneViewModel, Telefone>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(orig => orig.TipoCodigo.ToEnum<TipoTelefoneEnum>()));

            mapper.CreateMap<Telefone, TelefoneViewModel>()
                .ForMember(dest => dest.TipoCodigo, opt => opt.MapFrom(orig => orig.Tipo.Valor()));
        }
    }
}
