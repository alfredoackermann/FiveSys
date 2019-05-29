using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class TaraViewModel : BaseViewModel
    {
        public string Descricao { get; set; }
        public string Peso { get; set; }

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<TaraViewModel, Tara>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(orig => orig.Descricao));

            mapper.CreateMap<Tara, TaraViewModel>()               
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(orig => orig.Descricao))
                .ForMember(dest => dest.Peso, opt => opt.MapFrom(orig => orig.Peso.FormataValor()));
        }
    }
}
