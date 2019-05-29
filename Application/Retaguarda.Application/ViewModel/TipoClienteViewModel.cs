using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class TipoClienteViewModel : BaseViewModel
    {
        public string Descricao { get; set; }

        public static void Mapping(Profile Mapper)
        {
            Mapper.CreateMap<TipoClienteViewModel, TipoCliente>();
            Mapper.CreateMap<TipoCliente, TipoClienteViewModel>();
        }
    }
}
