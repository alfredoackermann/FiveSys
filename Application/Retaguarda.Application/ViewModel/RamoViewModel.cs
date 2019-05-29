using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class RamoViewModel : BaseViewModel
    {
        public string Descricao { get; set; }

        public static void Mapping(Profile Mapper)
        {
            Mapper.CreateMap<RamoViewModel, Ramo>();
            Mapper.CreateMap<Ramo, RamoViewModel>();
        }
    }
}
