using System.Collections.Generic;
using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class ParametroViewModel : BaseViewModel
    {
        public string Nome { get; set; }

        public string TipoCodigo { get; set; }
        public string TipoNome { get; set; }

        public string Valor { get; set; }

        public string Descricao { get; set; }

        #region Campos da tela

        public IDictionary<string, string> Tipos { get; set; }

        #endregion

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<ParametroViewModel, Parametro>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(orig => orig.TipoCodigo.ToEnum<ParametroTipoEnum>()))
                ;

            mapper.CreateMap<Parametro, ParametroViewModel>()
                .ForMember(dest => dest.TipoCodigo, opt => opt.MapFrom(orig => orig.Tipo.Valor()))
                .ForMember(dest => dest.TipoNome, opt => opt.MapFrom(orig => orig.Tipo.Descricao()))
                ;
            ;
        }
    }
}
